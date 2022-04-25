const addedPointsContainer = document.getElementById("addedPoints");
const branchesContainer = document.getElementById("branches");


function MoveElUp(currentEl) {
    let elementId = FindElementId(currentEl);
    let parentEl = currentEl.parentElement;

    console.log(currentEl);

    if (elementId != 0) {
        parentEl.insertBefore(currentEl, parentEl.children[elementId - 1]);
    }
}

function MoveElDown(currentEl) {
    let elementId = FindElementId(currentEl);
    let parentEl = currentEl.parentElement;

    console.log(currentEl);

    if (elementId != (parentEl.childElementCount - 1)) {
        parentEl.insertBefore(parentEl.children[elementId + 1], currentEl);
    }
}

function RemoveEl(currentEl) {
    let parentEl = currentEl.parentElement;
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

function ResizeInput(target) {
    target.style.width = (target.value.length + .5) + "ch";
}

function AddPoint(type) {
    //type = "main" || "branch"
    if (type === "main") {
        addedPointsContainer.insertBefore(PointBlock(addedPointsContainer.childElementCount + 1), null);
    }
    
}

function AddAttribute(target) {
    let parentEl = target.parentElement.parentElement;

    console.log(parentEl);

    let attrCont = parentEl.getElementsByClassName("addedAttributes")[0];
    console.log(attrCont);

    attrCont.insertBefore(AttributeBlock(), null);
}

function VisibleBtns(target, state) {
    let btnsEl = target.children[1];

    if (state) {
        btnsEl.style.visibility = "visible";
    } else {
        btnsEl.style.visibility = "hidden";
    }
}

function AddBranch(target) {
    let parentEl = target.parentElement;
    let branchDiv = document.createElement("div");
    branchDiv.style.width = "100%";
    branchDiv.style.marginTop = target.offsetTop;

    let p = document.createElement("p");
    p.textContent = "nowy div";

    branchDiv.appendChild(PointBlock(1));

    branchesContainer.appendChild(branchDiv);
}

var Points = {

}

var Point = {
    Attributes = {

    }
}

var Branch = {
    
}