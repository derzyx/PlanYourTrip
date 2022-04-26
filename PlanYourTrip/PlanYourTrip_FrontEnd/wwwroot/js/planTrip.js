const addedMainPointsContainer = document.getElementById("addedPoints");
const branchesContainer = document.getElementById("branches");
const defaultPointTitle = "Nowy punkt";


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
        btnsEl.style.visibility = "visible";
    } else {
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