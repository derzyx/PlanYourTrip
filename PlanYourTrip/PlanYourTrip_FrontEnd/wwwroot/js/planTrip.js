const addedMainPointsContainer = document.getElementById("addedPoints");
const branchesContainer = document.getElementById("branches");
const defaultPointTitle = "Nowy punkt";

var currentPointFocus;
var currentBranchFocus;

function MoveElUp(currentEl) {
    let elementId = FindElementId(currentEl);
    let parentEl = currentEl.parentElement;

    if (elementId != 0) {
        parentEl.insertBefore(currentEl, parentEl.children[elementId - 1]);
    }
}

function MoveElDown(currentEl) {
    let elementId = FindElementId(currentEl);
    let parentEl = currentEl.parentElement;

    if (elementId != (parentEl.childElementCount - 1)) {
        parentEl.insertBefore(parentEl.children[elementId + 1], currentEl);
    }
}

function RemoveEl(currentEl) {
    currentEl.remove();
}

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