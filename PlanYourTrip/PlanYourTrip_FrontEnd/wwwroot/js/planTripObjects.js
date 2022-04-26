var Points = []; //for main points

var pointPrototype = {
    Title: "",
    Attributes: {

    },
    IsBranch: false,
    HasBranch: false,
    HTMLEl: "",
    AddPoint: (sender) => {
        let point = new Point((defaultPointTitle + " " + (Points.length + 1)), false, false, null, Points.length);
        let pointBlock = PointBlock(point);
        let parentEl = sender.parentElement;
        parentEl.children[parentEl.childElementCount - 2].insertBefore(pointBlock, null);
        point.HTMLEl = pointBlock;
        Points.push(point);
        console.log(Points);
    },
    AddBranchPoint: (sender) => {
        let parentEl = sender.parentElement;
        let pointsContainer = parentEl.children[parentEl.childElementCount - 2];
        let point = new Point((defaultPointTitle + " " + (pointsContainer.childElementCount + 1)), true, false, null, pointsContainer.childElementCount);
        let pointBlock = PointBlock(point);
        pointsContainer.insertBefore(pointBlock, null);
        point.HTMLEl = pointBlock;

        let rootPointHTMLEl = Branch.prototype.FindRootPoint(sender.parentElement);
        let rootPoint = Point.prototype.FindPointInPoints(rootPointHTMLEl);

        rootPoint.BranchPoints.push(point);

        console.log(Points);
    },
    FindPointInPoints: (pointHTMLEL) => {
        for (let i = 0; i < Points.length; i++) {
            if (Points[i].HTMLEl === pointHTMLEL) {
                return Points[i];
            }
        }
    },
    DeleteMainPoint: (pointHTMLEl, isBranch) => {
        let thisPoint;
        if (!isBranch) {
            thisPoint = Point.prototype.FindPointInPoints(pointHTMLEl);

            if (thisPoint.HasBranch) {
                Branch.prototype.DeleteBranch(pointHTMLEl);
            }

            Points.splice(Points.indexOf(thisPoint), 1);
            console.log(Points);
        } else {
            rootDiv = Branch.prototype.FindRootPoint(pointHTMLEl.parentElement.parentElement);
            let rootPoint = Point.prototype.FindPointInPoints(rootDiv);
            rootPoint.BranchPoints.splice(rootPoint.BranchPoints.indexOf(pointHTMLEl, 1));
            console.log(Points);
        }

        pointHTMLEl.remove();
        
    },
    DeleteBranchPoint: (pointHTMLEl) => {

    }
}

function Point(title, isBranch, hasBranch, htmlEL, arrayId, branchPoints, branch) {
    this.Title = title;
    this.IsBranch = isBranch;
    this.HasBranch = hasBranch;
    this.HTMLEl = htmlEL;
    this.ArrayId = arrayId;
    this.Attributes = [];
    this.Branch = branch;
    this.BranchPoints = [];
}

Point.prototype = pointPrototype;
Point.prototype.constructor = Point;

var attributePrototype = {
    Key: "",
    Value: "",
    AddAttribute: (target) => {
        let parentEl = target.parentElement.parentElement;
        let attrCont = parentEl.getElementsByClassName("addedAttributes")[0];
        attrCont.insertBefore(AttributeBlock(attrCont.childElementCount + 1), null);
        Points[FindElementId(parentEl.parentElement)].Attributes.push(new Attribute(attrCont.childElementCount, "", (attrCont.childElementCount - 1)));
        console.log(Points);
    },
    UpdateAttribute: (key, value, id, target) => {
        let parentEl = target.parentElement.parentElement;
        Points[FindElementId(parentEl.parentElement)].Attributes[id].Key = key;
        Points[FindElementId(parentEl.parentElement)].Attributes[id].Value = value;
    },
    RemoveAttribute: (sender) => {
        let parentEl = sender.parentElement.parentElement;
        let attrCont = parentEl.getElementsByClassName("addedAttributes")[0];
        Points[FindElementId(parentEl.parentElement)].Attributes.splice(FindElementId(sender), 1)
        attrCont.children[FindElementId(sender)].remove();
    }
}

function Attribute(key, value, arrayId) {
    this.Key = key;
    this.Value = value;
    this.ArrayId = arrayId
}

Attribute.prototype = attributePrototype;
Attribute.prototype.constructor = Attribute;

var branchPrototype = {
    RootPoint: "",
    BranchEl: "",
    AddBranch: (rootEl) => {
        let branchDiv = BranchBlock();
        branchDiv.style.marginTop = (rootEl.offsetTop - 50) + "px";

        branchesContainer.appendChild(branchDiv);

        Points[FindElementId(rootEl)].HasBranch = true;
        Points[FindElementId(rootEl)].Branch = branchDiv;

        Branches.push(new Branch(rootEl, branchDiv));
        console.log(Points)
        console.log(Branches);
    },
    DeleteBranch: (rootEl) => {
        let branch = Branch.prototype.FindBranch(rootEl);
        let branchEl = branch.BranchEl;
        let rootPoint = Point.prototype.FindPointInPoints(rootEl);

        rootPoint.Branch = undefined;
        rootPoint.BranchPoints = [];
        rootPoint.HasBranch = false;

        console.log(branch, Branches.indexOf(branch));
        Branches.splice(Branches.indexOf(branch), 1);
        branchEl.remove();

        console.log(Branches);
        console.log(Points);
    },
    FindBranch: (rootELOrBranchEl) => {
        for (let i = 0; i < Branches.length; i++) {
            if (Branches[i].BranchEl === rootELOrBranchEl || Branches[i].RootPoint === rootELOrBranchEl) {
                return Branches[i];
            }
        }
    },
    FindRootPoint: (branch) => {
        for (let i = 0; i < Branches.length; i++) {
            if (Branches[i].BranchEl === branch) {
                return Branches[i].RootPoint;
            }
        }
    }
}

function Branch(rootPoint, branchEl) {
    this.RootPoint = rootPoint;
    this.BranchEl = branchEl;
}

Branch.prototype = branchPrototype;
Branch.prototype.constructor = Branch;

var Branches = []


// punkty w gałęzi dodawać do punktu korzenia (teraz dodaje do Points) - OK
// usuwanie punktów/gałęzi - OK
// wyłączenie przycisku "dodaj gałąź" jeśli już jest gałąź - OK

// linia łącząca rootPoint z gałęzią
// chowanie/pokazywanie bloku gałęzi i guzik do tego
// chowanie poprzedniego bloku gałęzi jeśli kliknięto inny guzik lub "zwiń gałąź/powrót do root"
// atrybuty w głównym punkcie średnio działają trzeba zrobić na sztywno coś