function PointBlock(blockNumber) {
    //<mainDiv>
    //  <pointDiv>
    //      <contents/>
    //      <addedAttrsDiv/>
    //      <btnsDiv>
    //          <btnUp/>
    //          <btnDown/>
    //          <btnAddAttr/>
    //      </btnsDiv>
    //  </pointDiv>
    //  <lineDiv>
    //  </lineDiv>
    //  <addBranchDiv>
    //      <btnAddBranch/>
    //  </addBranchDiv>
    //</mainDiv>

    let mainDiv = document.createElement("div");
    mainDiv.className = "col-8 pointContainer";

    let pointDiv = document.createElement("div");
    pointDiv.className = "point";

    let contents = document.createElement("p");
    contents.textContent = blockNumber;

    let addedAttrsDiv = document.createElement("div");
    addedAttrsDiv.className = "addedAttributes";

    let btnUp = document.createElement("button");
    let btnDown = document.createElement("button");
    let btnAddAttr = document.createElement("button");
    btnUp.textContent = "Up";
    btnUp.addEventListener("click", function (e) { MoveElUp(this.parentElement.parentElement.parentElement) });
    btnDown.textContent = "Down";
    btnDown.addEventListener("click", function (e) { MoveElDown(this.parentElement.parentElement.parentElement) });
    btnAddAttr.textContent = "+ dodaj atrybut";
    btnAddAttr.addEventListener("click", function (e) { AddAttribute(this) })

    let buttonsDiv = document.createElement("div");

    buttonsDiv.appendChild(btnUp);
    buttonsDiv.appendChild(btnDown);
    buttonsDiv.appendChild(btnAddAttr);

    pointDiv.appendChild(contents);
    pointDiv.appendChild(addedAttrsDiv);
    pointDiv.appendChild(buttonsDiv);

    let lineDiv = document.createElement("div");
    lineDiv.className = "line"

    let addBranchDiv = document.createElement("div");
    addBranchDiv.className = "addBranchBtn";
    let btnAddBranch = document.createElement("button");
    btnAddBranch.className = "btn btn-outline-secondary";
    btnAddBranch.innerHTML = `
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"/>
        </svg>
        dodaj gałąź`;
    btnAddBranch.addEventListener("click", function (e) { AddBranch(this.parentElement.parentElement) });

    addBranchDiv.appendChild(btnAddBranch);

    mainDiv.appendChild(pointDiv);
    mainDiv.appendChild(lineDiv);
    mainDiv.appendChild(addBranchDiv);

    return mainDiv;
}

function AttributeBlock() {
    //<mainDiv>
    //  <inputDiv>
    //      <keyInput/>
    //      <valueInput/>
    //  </inputDiv>
    //  <btnsDiv>
    //      <btnGroup>
    //          <btnUp/>
    //          <btnDown/>
    //          <btnCross/>
    //      </btnGroup>
    //  <btnsDiv>
    //</mainDiv>



    let mainDiv = document.createElement("div");
    mainDiv.className = "row pointAttr";
    mainDiv.addEventListener("mouseover", function (e) { VisibleBtns(this,true) })
    mainDiv.addEventListener("mouseout", function (e) { VisibleBtns(this,false) })

    let inputDiv = document.createElement("div");
    inputDiv.className = "col";

    let keyInput = document.createElement("input");
    keyInput.className = "pointAttrKey";
    keyInput.value = "klucz";
    keyInput.addEventListener("input", function (e) { ResizeInput(e.target) });
    keyInput.style.width = "6ch";

    let valueInput = document.createElement("input");
    valueInput.className = "pointAttrValue";
    valueInput.value = "wartość";
    valueInput.addEventListener("input", function (e) { ResizeInput(e.target) });
    valueInput.style.width = "8ch";

    inputDiv.appendChild(keyInput);
    inputDiv.appendChild(document.createTextNode(": "));
    inputDiv.appendChild(valueInput);

    //--------------------------------------------------------

    let btnsDiv = document.createElement("div");
    btnsDiv.className = "col justify-content-end attrBtns";
    btnsDiv.style.visibility = "hidden";

    let btnsGroup = document.createElement("div")
    btnsGroup.className = "btn-group";

    let btnUp = document.createElement("button");
    btnUp.type = "button";
    btnUp.className = "btn btn-outline-secondary attrBtn";
    btnUp.addEventListener("click", function (e) { MoveElUp(this.parentElement.parentElement.parentElement) });
    btnUp.innerHTML = `
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-up-short" viewBox="0 0 16 16">
            <path fill-rule="evenodd" d="M8 12a.5.5 0 0 0 .5-.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 .5.5z" />
        </svg>`;

    let btnDown = document.createElement("button");
    btnDown.type = "button";
    btnDown.className = "btn btn-outline-secondary attrBtn";
    btnDown.addEventListener("click", function (e) { MoveElDown(this.parentElement.parentElement.parentElement) });
    btnDown.innerHTML = `
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-down-short" viewBox="0 0 16 16">
            <path fill-rule="evenodd" d="M8 4a.5.5 0 0 1 .5.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5A.5.5 0 0 1 8 4z" />
        </svg>`;

    let btnCross = document.createElement("button");
    btnCross.type = "button";
    btnCross.className = "btn btn-outline-secondary attrBtn";
    btnCross.addEventListener("click", function (e) { RemoveEl(this.parentElement.parentElement.parentElement) });
    btnCross.innerHTML = `
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
            <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z" />
        </svg>`;

    btnsGroup.appendChild(btnUp);
    btnsGroup.appendChild(btnDown);
    btnsGroup.appendChild(btnCross);

    btnsDiv.appendChild(btnsGroup);

    //--------------------------------------------------------

    mainDiv.appendChild(inputDiv);
    mainDiv.appendChild(btnsDiv);

    return mainDiv;
}