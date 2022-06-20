var Trip = [];

function Options(planName, askBeforeDelete) {
    this.PlanName = planName;
    this.AskBeforeDelete = askBeforeDelete;
}

var Points = []; //for main points

var pointPrototype = {
    Title: "",
    Attributes: {

    },
    IsBranch: false,
    HasBranch: false,
    HTMLEl: "",
    AddPoint: (sender) => {
        let point = new Point((defaultPointTitle + " " + (Points.length + 1)), false, false, null, Points.length, null, null, "#ffffff");
        let pointBlock = PointBlock(point);
        let parentEl = sender.parentElement;
        parentEl.children[parentEl.childElementCount - 2].insertBefore(pointBlock, null);
        point.HTMLEl = pointBlock;
        Points.push(point);
    },
    AddBranchPoint: (sender) => {
        let parentEl = sender.parentElement;
        let pointsContainer = parentEl.children[parentEl.childElementCount - 2];
        let point = new Point((defaultPointTitle + " " + (pointsContainer.childElementCount + 1)), true, false, null, pointsContainer.childElementCount, null, null, "#ffffff");
        let pointBlock = PointBlock(point);
        pointsContainer.insertBefore(pointBlock, null);
        point.HTMLEl = pointBlock;

        let rootPointHTMLEl = Branch.prototype.FindRootPoint(sender.parentElement);
        let rootPoint = Point.prototype.FindPointInPoints(rootPointHTMLEl);

        rootPoint.BranchPoints.push(point);
    },
    FindPointInPoints: (pointHTMLEL) => {
        for (let i = 0; i < Points.length; i++) {
            if (Points[i].HTMLEl === pointHTMLEL) {
                return Points[i];
            }
        }
    },
    DeletePoint: (pointHTMLEl, isBranch) => {
        let thisPoint;
        if (!isBranch) {
            thisPoint = Point.prototype.FindPointInPoints(pointHTMLEl);

            if (thisPoint.HasBranch) {
                Branch.prototype.DeleteBranch(pointHTMLEl);
            }

            Points.splice(Points.indexOf(thisPoint), 1);
        } else {
            let rootDiv = Branch.prototype.FindRootPoint(pointHTMLEl.parentElement.parentElement);
            let rootPoint = Point.prototype.FindPointInPoints(rootDiv);
            rootPoint.BranchPoints.splice(rootPoint.BranchPoints.indexOf(Branch.prototype.FindBranchPoint(pointHTMLEl)), 1);
        }

        pointHTMLEl.remove();

        for (let i = 0; i < Points.length; i++) {
            UpdateBranchOffset(Points[i]);
        }
    },
    MovePoint: (pointHTMLEl, direction) => {
        let elementId = FindElementId(pointHTMLEl);
        let parentEl = pointHTMLEl.parentElement;
        let point = Point.prototype.FindPointInPoints(pointHTMLEl);
        let fromIndex = Points.indexOf(point);

        if (direction === "up" && (elementId != 0)) {
            Points.splice(fromIndex, 1);
            Points.splice((fromIndex - 1), 0, point);
            parentEl.insertBefore(pointHTMLEl, parentEl.children[elementId - 1]);
            UpdateBranchOffset(Points[Points.indexOf(point) + 1]);
            UpdateBranchOffset(Points[Points.indexOf(point)]);
        }
        else if (direction === "down" && (elementId != (parentEl.childElementCount - 1))) {
            Points.splice(fromIndex, 1);
            Points.splice((fromIndex + 1), 0, point);
            parentEl.insertBefore(parentEl.children[elementId + 1], pointHTMLEl);
            UpdateBranchOffset(Points[Points.indexOf(point) - 1]);
            UpdateBranchOffset(Points[Points.indexOf(point)]);
        }
    },
    MoveBranchPoint: (branchHTMLEl, direction) => {
        let branch = Branch.prototype.FindBranch(branchHTMLEl.parentElement.parentElement);
        let point = Point.prototype.FindPointInPoints(branch.RootPoint);
        let elementId = FindElementId(branchHTMLEl);
        let parentEl = branchHTMLEl.parentElement;
        let branchPoint = Branch.prototype.FindBranchPoint(branchHTMLEl);
        let fromIndex = point.BranchPoints.indexOf(branchPoint);

        if (direction == "up" && (elementId != 0)) {

            point.BranchPoints.splice(fromIndex, 1);
            point.BranchPoints.splice((fromIndex - 1), 0, branchPoint);

            parentEl.insertBefore(branchHTMLEl, parentEl.children[elementId - 1]);
        }
        else if (direction == "down" && (elementId != (parentEl.childElementCount - 1))) {
            point.BranchPoints.splice(fromIndex, 1);
            point.BranchPoints.splice((fromIndex + 1), 0, branchPoint);

            parentEl.insertBefore(parentEl.children[elementId + 1], branchHTMLEl);
        }
    }
}

function Point(title, isBranch, hasBranch, htmlEL, arrayId, branchPoints, branch, backgroundColor) {
    this.Title = title;
    this.IsBranch = isBranch;
    this.HasBranch = hasBranch;
    this.HTMLEl = htmlEL;
    this.ArrayId = arrayId;
    this.Attributes = [];
    this.Branch = branch;
    this.BranchPoints = [];
    this.BackgroundColor = backgroundColor;
}

Point.prototype = pointPrototype;
Point.prototype.constructor = Point;

var attributePrototype = {
    Key: "",
    Value: "",
    AddAttribute: (parentPoint) => {
        let attrCont = parentPoint.HTMLEl.getElementsByClassName("addedAttributes")[0];
        let attribute = new Attribute(("klucz " + (attrCont.childElementCount + 1)), "", "", attrCont.childElementCount, "attr", null);
        let attrBlock = AttributeBlock(attribute);

        attrCont.insertBefore(attrBlock, null);
        attribute.HTMLEl = attrBlock;
        parentPoint.Attributes.push(attribute);
    },
    AddLinkAttribute: (parentPoint, attribute) => {
        let attrCont = parentPoint.HTMLEl.getElementsByClassName("addedAttributes")[0];

        if (attribute.Key === "") {
            attribute.Key = "klucz" + (attrCont.childElementCount + 1);
        }
        attribute.ArrayId = attrCont.childElementCount;

        let attrBlock = AttributeBlock(attribute);
        attrCont.insertBefore(attrBlock, null);
        attribute.HTMLEl = attrBlock;

        parentPoint.Attributes.push(attribute);
    },
    RemoveAttribute: (sender) => {
        let parentPoint = FindParentPoint(sender);

        let attrCont = parentPoint.HTMLEl.getElementsByClassName("addedAttributes")[0];
        parentPoint.Attributes.splice(FindElementId(sender), 1)
        attrCont.children[FindElementId(sender)].remove();
    },
    MoveAttribute: (sender, direction) => {
        let parentEl = sender.parentElement;
        let parentPoint = FindParentPoint(sender);

        let elementId = FindElementId(sender);
        let attribute = Attribute.prototype.FindAttributeObj(sender);
        let fromIndex = parentPoint.Attributes.indexOf(attribute);

        if (direction == "up" && (elementId != 0)) {
            parentPoint.Attributes.splice(fromIndex, 1);
            parentPoint.Attributes.splice((fromIndex - 1), 0, attribute);
            parentEl.insertBefore(sender, parentEl.children[elementId - 1]);
        }
        else if (direction == "down" && (elementId != (parentEl.childElementCount - 1))) {
            parentPoint.Attributes.splice(fromIndex, 1);
            parentPoint.Attributes.splice((fromIndex + 1), 0, attribute);
            parentEl.insertBefore(parentEl.children[elementId + 1], sender);
        }
    },
    FindAttributeObj: (attrHTMLEl) => {
        let parentPoint = FindParentPoint(attrHTMLEl);

        for (let i = 0; i < parentPoint.Attributes.length; i++) {
            if (parentPoint.Attributes[i].HTMLEl === attrHTMLEl) {
                return parentPoint.Attributes[i];
            }
        }
    }
}

function Attribute(key, value, hiddenVal, arrayId, type, htmlEl) {
    this.Key = key;
    this.Value = value;
    this.HiddenVal = hiddenVal;
    this.HTMLEl = htmlEl;
    this.Type = type;
    this.ArrayId = arrayId;
}

Attribute.prototype = attributePrototype;
Attribute.prototype.constructor = Attribute;

var branchPrototype = {
    RootPoint: "",
    BranchEl: "",
    AddBranch: (rootEl) => {
        let branchDiv = BranchBlock();
        branchDiv.style.marginTop = (rootEl.offsetTop - 120) + "px";

        branchesContainer.appendChild(branchDiv);
        
        let parentPoint = Point.prototype.FindPointInPoints(rootEl);
        parentPoint.HasBranch = true;
        parentPoint.Branch = branchDiv;

        Branches.push(new Branch(rootEl, branchDiv));
    },
    DeleteBranch: (rootEl) => {
        let branch = Branch.prototype.FindBranch(rootEl);
        let branchEl = branch.BranchEl;
        let rootPoint = Point.prototype.FindPointInPoints(rootEl);

        rootPoint.Branch = undefined;
        rootPoint.BranchPoints = [];
        rootPoint.HasBranch = false;

        Branches.splice(Branches.indexOf(branch), 1);
        branchEl.remove();
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
    },
    FindBranchPoint: (branchPointHTMLEl) => {
        let root = Branch.prototype.FindRootPoint(branchPointHTMLEl.parentElement.parentElement);
        let rootPoint = Point.prototype.FindPointInPoints(root);

        for (let i = 0; i < rootPoint.BranchPoints.length; i++) {
            if (rootPoint.BranchPoints[i].HTMLEl === branchPointHTMLEl) {
                return rootPoint.BranchPoints[i];
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


function FindParentPoint(sender) {
    let parentPoint;

    if (Point.prototype.FindPointInPoints(sender.parentElement.parentElement.parentElement)) {
        parentPoint = Point.prototype.FindPointInPoints(sender.parentElement.parentElement.parentElement);
    }
    else if (Branch.prototype.FindBranchPoint(sender.parentElement.parentElement.parentElement)) {
        parentPoint = Branch.prototype.FindBranchPoint(sender.parentElement.parentElement.parentElement);
    }

    return parentPoint;
}