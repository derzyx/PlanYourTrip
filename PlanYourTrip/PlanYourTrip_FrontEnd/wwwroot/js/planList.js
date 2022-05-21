

function OpenDeleteWindow(postId) {
    document.getElementById("deleteContainer").style.visibility = "visible";
    document.getElementById("postIdField").value = postId;
}

function HideDeleteWindow() {
    document.getElementById("deleteContainer").style.visibility = "hidden";
    document.getElementById("postIdField").value = 0;
}
