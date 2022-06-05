

function OpenDeleteWindow(entityId) {
    document.getElementById("deleteContainer").style.visibility = "visible";
    document.getElementById("entityIdField").value = entityId;
}

function HideDeleteWindow() {
    document.getElementById("deleteContainer").style.visibility = "hidden";
    document.getElementById("entityIdField").value = 0;
}
