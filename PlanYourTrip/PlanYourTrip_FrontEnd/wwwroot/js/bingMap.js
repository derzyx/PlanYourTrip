const outputDiv = document.getElementById("mapOutput");

var sdsDataSourceUrl = "https://spatial.virtualearth.net/REST/v1/data/Microsoft/PointsOfInterest";
var map;
var gridLayer;

var currentPlace;

var placesTable = ['5999', '4581'];

function GetMap() {
    gridLayer = new Microsoft.Maps.Layer();
    var navigationBarMode = Microsoft.Maps.NavigationBarMode;
    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        navigationBarMode: navigationBarMode.square,
        supportedMapTypes: [Microsoft.Maps.MapTypeId.road, Microsoft.Maps.MapTypeId.aerial]
    });

    Microsoft.Maps.loadModule('Microsoft.Maps.SpatialDataService', function () {
        Microsoft.Maps.Events.addHandler(map, 'viewchangeend', function () {
            getLocationsInView();
            gridLayer.clear();
        });
        getLocationsInView();
    })
}

function getLocationsInView() {
    let sds = Microsoft.Maps.SpatialDataService;

    if (map.getZoom() >= 12) {


        var queryOptions = {
            queryUrl: sdsDataSourceUrl,
            top: 25,
            spatialFilter: {
                spatialFilterType: 'nearby',
                location: map.getCenter(),
                radius: 25
            },
            filter: new sds.Filter('EntityTypeID', sds.FilterCompareOperator.isIn, placesTable)
        };

        Microsoft.Maps.SpatialDataService.QueryAPIManager.search(queryOptions, map, function (data) {
            map.entities.clear();
            for (let i = 0; i < data.length; i++) {
                data[i].entity.title = data[i].metadata.Name;
                Microsoft.Maps.Events.addHandler(data[i], 'click', pushpinClicked);
            }
            map.entities.push(data);
        });
    }
    else {
        map.entities.clear();
    }
}

function pushpinClicked(e) {
    if (placeLabel.style.visibility === "hidden") {
        placeLabel.style.visibility = "visible";
    }
    if (e.target.metadata) {
        currentPlace = e.target;
        outputDiv.textContent = currentPlace.entity.title;
    }
}

function getPointByEntityId(id) {
    var queryOptions = {
        queryUrl: "https://spatial.virtualearth.net/REST/v1/data/Microsoft/PointsOfInterest",
        filter: new Microsoft.Maps.SpatialDataService.Filter("entityId", "eq", id)
    };

    Microsoft.Maps.SpatialDataService.QueryAPIManager.search(queryOptions, map, function (data) {
        console.log(data);
        map.setView({
            center: new Microsoft.Maps.Location(data[0].geometry.y, data[0].geometry.x),
            zoom: 15
        });
    });
}