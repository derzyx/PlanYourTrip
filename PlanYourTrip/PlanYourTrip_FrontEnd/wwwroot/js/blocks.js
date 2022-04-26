const upIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-chevron-up" viewBox="0 0 16 16">
                  <path fill-rule="evenodd" d="M7.646 4.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1-.708.708L8 5.707l-5.646 5.647a.5.5 0 0 1-.708-.708l6-6z"/>
                </svg>`;
const downIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-chevron-down" viewBox="0 0 16 16">
                    <path fill-rule="evenodd" d="M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z"/>
                  </svg>`;
const crossIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                      <path fill-rule="evenodd" d="M13.854 2.146a.5.5 0 0 1 0 .708l-11 11a.5.5 0 0 1-.708-.708l11-11a.5.5 0 0 1 .708 0Z"/>
                      <path fill-rule="evenodd" d="M2.146 2.146a.5.5 0 0 0 0 .708l11 11a.5.5 0 0 0 .708-.708l-11-11a.5.5 0 0 0-.708 0Z"/>
                    </svg>`;
const colorIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-palette" viewBox="0 0 16 16">
                      <path d="M8 5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zm4 3a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zM5.5 7a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm.5 6a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z"/>
                      <path d="M16 8c0 3.15-1.866 2.585-3.567 2.07C11.42 9.763 10.465 9.473 10 10c-.603.683-.475 1.819-.351 2.92C9.826 14.495 9.996 16 8 16a8 8 0 1 1 8-8zm-8 7c.611 0 .654-.171.655-.176.078-.146.124-.464.07-1.119-.014-.168-.037-.37-.061-.591-.052-.464-.112-1.005-.118-1.462-.01-.707.083-1.61.704-2.314.369-.417.845-.578 1.272-.618.404-.038.812.026 1.16.104.343.077.702.186 1.025.284l.028.008c.346.105.658.199.953.266.653.148.904.083.991.024C14.717 9.38 15 9.161 15 8a7 7 0 1 0-7 7z"/>
                    </svg>`;
const plusIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-lg" viewBox="0 0 16 16">
                      <path fill-rule="evenodd" d="M8 2a.5.5 0 0 1 .5.5v5h5a.5.5 0 0 1 0 1h-5v5a.5.5 0 0 1-1 0v-5h-5a.5.5 0 0 1 0-1h5v-5A.5.5 0 0 1 8 2Z"/>
                    </svg>`;


function PointBlock(point) {
    //<mainDiv>
    //  <pointDiv>
    //      <title/>
    //      <addedAttrsDiv/>
    //      <addAttrDiv>
    //          <btnAddAttr/>
    //      </addAttrDiv>
    //  </pointDiv>
    //  <managePointDiv>
    //      <manageBtnsGroup>
    //          <btnDelete/>
    //          <btnUp/>
    //          <btnDown/>
    //          <btnBgColor/>
    //      </manageBtnsGroup>
    //  </managePointDiv>
    //  </lineDiv>
    //  <addBranchDiv>
    //      <btnAddBranch/>
    //  </addBranchDiv>
    //</mainDiv>

    let mainDiv = document.createElement("div");
    mainDiv.className = "col-8 pointContainer";

    let pointDiv = document.createElement("div");
    pointDiv.className = "point";

    let title = document.createElement("input");
    title.className = "pointTitle";
    title.value = point.Title;

    let addedAttrsDiv = document.createElement("div");
    addedAttrsDiv.className = "addedAttributes";

    let btnAddAttr = document.createElement("button");
    btnAddAttr.className = "btn btn-outline-secondary tinyButton";
    btnAddAttr.innerHTML = plusIcon + "dodaj atrybut";
    btnAddAttr.addEventListener("click", function (e) { Attribute.prototype.AddAttribute(this) })

    let addAttrDiv = document.createElement("div");

    addAttrDiv.appendChild(btnAddAttr);

    pointDiv.appendChild(title);
    pointDiv.appendChild(addedAttrsDiv);
    pointDiv.appendChild(addAttrDiv);

    //---------------------------------------------------------------
    let managePointDiv = document.createElement("div");
    managePointDiv.className = "managePoint"

    let manageBtnsGroup = document.createElement("div");
    manageBtnsGroup.className = "btn-group managePointBtns";

    let btnUp = document.createElement("button");
    let btnDown = document.createElement("button");
    let btnDelete = document.createElement("button");
    let btnBgColor = document.createElement("button");

    btnUp.className = "btn btn-outline-secondary tinyButton";
    btnUp.addEventListener("click", function (e) { MoveElUp(this.parentElement.parentElement.parentElement) });
    btnUp.innerHTML = upIcon;

    btnDown.className = "btn btn-outline-secondary tinyButton";
    btnDown.addEventListener("click", function (e) { MoveElDown(this.parentElement.parentElement.parentElement) });
    btnDown.innerHTML = downIcon;

    btnDelete.className = "btn btn-outline-danger tinyButton";
    btnDelete.addEventListener("click", function (e) { Point.prototype.DeleteMainPoint(this.parentElement.parentElement.parentElement, point.IsBranch) })
    btnDelete.innerHTML = crossIcon;

    btnBgColor.className = "btn btn-outline-secondary tinyButton";
    //EVENT
    btnBgColor.innerHTML = colorIcon;


    manageBtnsGroup.appendChild(btnDelete);
    manageBtnsGroup.appendChild(btnUp);
    manageBtnsGroup.appendChild(btnDown);
    manageBtnsGroup.appendChild(btnBgColor);
    

    managePointDiv.appendChild(manageBtnsGroup);
    //-------------------------------------------------------------------


    mainDiv.appendChild(pointDiv);
    mainDiv.appendChild(managePointDiv);
    
    let branchManageBtns = document.createElement("div");
    branchManageBtns.className = "branchManageBtns";

    if (point.IsBranch == false) {
        let btnAddBranch = document.createElement("button");
        btnAddBranch.className = "btn btn-outline-secondary tinyButton";
        btnAddBranch.innerHTML = plusIcon + "dodaj gałąź";
        btnAddBranch.addEventListener("click", function (e) { Branch.prototype.AddBranch(this.parentElement.parentElement); AddAndRemoveBranchVisibility(this) });

        branchManageBtns.appendChild(btnAddBranch);
    }

    let btnDeleteBranch = document.createElement("button");
    btnDeleteBranch.className = "btn btn-outline-danger tinyButton";
    btnDeleteBranch.innerHTML = crossIcon + "usuń gałąź";
    btnDeleteBranch.style.visibility = "hidden";
    btnDeleteBranch.addEventListener("click", function (e) { Branch.prototype.DeleteBranch(this.parentElement.parentElement); AddAndRemoveBranchVisibility(this) });

    branchManageBtns.appendChild(btnDeleteBranch);

    mainDiv.appendChild(branchManageBtns);

    return mainDiv;
}

function AttributeBlock(titleNumber) {
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
    keyInput.value = "klucz " + titleNumber;
    keyInput.addEventListener("input", function (e) { ResizeInput(e.target) });
    keyInput.style.width = "8ch";

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
    btnUp.className = "btn btn-outline-secondary attrBtn tinyButton";
    btnUp.addEventListener("click", function (e) { MoveElUp(this.parentElement.parentElement.parentElement) });
    btnUp.innerHTML = upIcon;

    let btnDown = document.createElement("button");
    btnDown.type = "button";
    btnDown.className = "btn btn-outline-secondary attrBtn tinyButton";
    btnDown.addEventListener("click", function (e) { MoveElDown(this.parentElement.parentElement.parentElement) });
    btnDown.innerHTML = downIcon;

    let btnCross = document.createElement("button");
    btnCross.type = "button";
    btnCross.className = "btn btn-outline-danger attrBtn tinyButton";
    //btnCross.addEventListener("click", function (e) { RemoveEl(this.parentElement.parentElement.parentElement) });
    btnCross.addEventListener("click", function (e) { Attribute.prototype.RemoveAttribute(this.parentElement.parentElement.parentElement) });
    btnCross.innerHTML = crossIcon;

    btnsGroup.appendChild(btnUp);
    btnsGroup.appendChild(btnDown);
    btnsGroup.appendChild(btnCross);

    btnsDiv.appendChild(btnsGroup);

    //--------------------------------------------------------

    mainDiv.appendChild(inputDiv);
    mainDiv.appendChild(btnsDiv);

    return mainDiv;
}

function BranchBlock() {
    let branchDiv = document.createElement("div");
    branchDiv.className = "branchContainer";

    let pointsDiv = document.createElement("div");
    pointsDiv.className = "addedPoints";

    let addPointDiv = document.createElement("div");
    addPointDiv.className = "col-8 point addPoint";
    addPointDiv.addEventListener("click", function (e) { Point.prototype.AddBranchPoint(this) })

    let p = document.createElement("p");
    p.textContent = "+ Dodaj punkt";

    //pointsDiv.appendChild(PointBlock());

    addPointDiv.appendChild(p);

    branchDiv.appendChild(pointsDiv);
    branchDiv.appendChild(addPointDiv);

    return branchDiv;
}