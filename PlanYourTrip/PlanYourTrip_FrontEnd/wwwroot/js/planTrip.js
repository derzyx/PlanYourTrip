const addedMainPointsContainer = document.getElementById("addedPoints");
const branchesContainer = document.getElementById("branches");
const defaultPointTitle = "Nowy punkt";

const addLinkBox = document.getElementById("addLinkBox");
const addLinkBtn = document.getElementById("addLink");
const closeLinkBoxBtn = document.getElementById("closeLinkBox");
const linkValueInput = document.getElementById("linkValue");
const linkURLInput = document.getElementById("linkHiddenVal");

const removeQuestionBox = document.getElementById("removeElQuestion");
const closeQuestionBtn = document.getElementById("closeQuestionBox");
const removeElBtn = document.getElementById("removeEl");

const pointTypesDDL = document.getElementById("pointTypesDDL");

const placeLabel = document.getElementById("placeLabel");

const askBeforeDeleteBool = document.getElementById("askBeforeDeleteBool");

var currentPointFocus;
var currentBranchFocus;

// EVENTS

closeLinkBoxBtn.addEventListener("click", function (e) {
    addLinkBox.style.visibility = "hidden";
});

addLinkBtn.addEventListener("click", function (e) {
    let attribute = new Attribute("", linkValueInput.value, linkURLInput.value, null, "link", null);
    let sourcePoint = Points[e.target.getAttribute("source")];
    Attribute.prototype.AddLinkAttribute(sourcePoint, attribute);
    addLinkBox.style.visibility = "hidden";
});

pointTypesDDL.addEventListener("change", function (e) {
    console.log(e.target.value);
});

// FUNCTIONS

function FindElementId(element) {
    let parentEl = element.parentElement;
    for (let i = 0; i <= parentEl.children.length; i++) {
        if (element == parentEl.children[i]) {
            return i;
        }
    }
}

function ResizeInput(sender) {
    sender.style.width = (sender.value.length + .5) + "ch";
}


function VisibleBtns(sender, state) {
    let btnsEl = sender.children[1];

    if (state) {
        sender.children[0].children[0].style.fontWeight = "bold";
        sender.children[0].children[1].style.fontWeight = "bold";
        btnsEl.style.visibility = "visible";
    } else {
        sender.children[0].children[0].style.fontWeight = "normal";
        sender.children[0].children[1].style.fontWeight = "normal";
        btnsEl.style.visibility = "hidden";
    }
}

function AddAndRemoveBranchVisibility(sender, state) {
    sender.style.visibility = "hidden";

    if (sender.parentElement.children[0] === sender) {
        sender.parentElement.children[1].style.visibility = "visible";
    } else {
        sender.parentElement.children[0].style.visibility = "visible";
    }
}

function ColorPick(sender) {
    let pointParent = sender.parentElement.parentElement.parentElement;
    pointParent.children[0].style.backgroundColor = sender.value;
}

function ChangePointFocus(point, isBranch) {
    if (currentPointFocus !== undefined) {
        currentPointFocus.HTMLEl.children[0].className = "point"
    }
    if (isBranch) {
        currentPointFocus = Branch.prototype.FindBranchPoint(point.parentElement);
    }
    else {
        currentPointFocus = Point.prototype.FindPointInPoints(point.parentElement);
    }

    currentPointFocus.HTMLEl.children[0].className = "point currentPoint";
    console.log(currentPointFocus);
}

function ChangeBranchFocus(sender) {
    let branch = Branch.prototype.FindBranch(sender);
    let branchEl = branch.BranchEl;
    console.log(branchEl);

    for (let i = 0; i < Branches.length; i++) {
        if (Branches[i].BranchEl === branchEl) {
            branchEl.style.visibility = (branchEl.style.visibility === "hidden" || branchEl.style.visibility === "") ? "visible" : "hidden";
        } else {
            Branches[i].BranchEl.style.visibility = "hidden";
        }
    }
}

function UpdateBranchOffset(point) {
    console.log(point);
    if (point.HasBranch) {
        point.Branch.style.marginTop = (point.HTMLEl.offsetTop - 120) + "px";
    }
}

function OpenAddLinkBox(sender) {
    addLinkBox.style.visibility = "visible";
    let parentPointEl = sender.parentElement.parentElement.parentElement;
    let parentPointId = Points.indexOf(Point.prototype.FindPointInPoints(parentPointEl));
    addLinkBtn.setAttribute("source", parentPointId);
}

function OpenRemoveElQuestionBox(sender, type) {
    if (askBeforeDeleteBool.checked) {
        document.querySelector("body").insertBefore(RemoveElQuestionBlock(sender, type), document.getElementById("navBar"));
    }
    else {
        RemoveByType(sender, type);
    }
}

function RemoveByType(sender, type) {
    switch (type) {
        case "point":
            Point.prototype.DeletePoint(sender, false);
            break;
        case "branch-point":
            Point.prototype.DeletePoint(sender, true);
            break;
        case "branch":
            Branch.prototype.DeleteBranch(sender);
            break;
        case "attribute":
            Attribute.prototype.RemoveAttribute(sender);
            break;
    }
}

function addPlaceToPoint() {
    console.log(currentPlace);
    console.log(currentPointFocus);
    if (currentPointFocus != undefined && currentPlace != undefined) {
        let attribute = new Attribute("Miejsce", currentPlace.entity.title, currentPlace.metadata.EntityID, null, "map-link", null);
        //let currentPoint = Point.prototype.FindPointInPoints(currentPointFocus.parentElement);

        Attribute.prototype.AddLinkAttribute(currentPointFocus, attribute);
    }
    else {
        alert("Nie wybrano punktu podróży!");
    }
}

function closeLabelBox() {
    if (placeLabel.style.visibility === "visible") {
        placeLabel.style.visibility = "hidden";
    }
}

function saveValues() {

    //główne punkty
    for (let p = 0; p < Points.length; p++) {

        //tytuły głównych punktów
        let pointTitle = Points[p].HTMLEl.getElementsByClassName("pointTitle")[0].value;
        Points[p].Title = pointTitle;

        //atrybuty w głównych punktach
        if (Points[p].Attributes.length > 0) {
            for (let a = 0; a < Points[p].Attributes.length; a++) {
                let attrKey = Points[p].Attributes[a].HTMLEl.getElementsByClassName("attrKey")[0].value;
                Points[p].Attributes[a].Key = attrKey;

                if (Points[p].Attributes[a].Type === "attr") {
                    let attrValue = Points[p].Attributes[a].HTMLEl.getElementsByClassName("attrValue")[0].value;
                    Points[p].Attributes[a].Value = attrValue;
                }
            }
        }

        //podpunkty w głównych punkach
        if (Points[p].HasBranch) {
            for (let bp = 0; bp < Points[p].BranchPoints.length; bp++) {

                //tytuły podpunktów
                let pointTitle = Points[p].BranchPoints[bp].HTMLEl.getElementsByClassName("pointTitle")[0].value;
                Points[p].BranchPoints[bp].Title = pointTitle;

                //atrybuty w podpunktach
                if (Points[p].BranchPoints[bp].Attributes.length > 0) {
                    for (let a = 0; a < Points[p].BranchPoints[bp].Attributes.length; a++) {
                        let currentAttr = Points[p].BranchPoints[bp].Attributes[a];
                        let attrKey = currentAttr.HTMLEl.getElementsByClassName("attrKey")[0].value;
                        currentAttr.Key = attrKey;

                        if (currentAttr.Type === "attr") {
                            let attrValue = currentAttr.HTMLEl.getElementsByClassName("attrValue")[0].value;
                            currentAttr.Value = attrValue;
                        }
                    }
                }
            }
        }
    }

    return JSON.stringify(Points);
}