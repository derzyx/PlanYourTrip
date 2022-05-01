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

//closeQuestionBtn.addEventListener("click", function (e) {
//    removeQuestionBox.style.visibility = "hidden";
//});

//removeElBtn.addEventListener("click", function (e) {

//})


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

function ChangePointFocus(point) {
    if (currentPointFocus !== undefined) {
        currentPointFocus.className = "point"
        currentPointFocus = point;
    } else {
        currentPointFocus = point;
    }
    currentPointFocus.className = "point currentPoint";
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

    // TA ZMIENNA MA BYĆ JAKO PRZYCISK W NAVBAR
    let askBeforeDelete = true;

    if (askBeforeDelete) {
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