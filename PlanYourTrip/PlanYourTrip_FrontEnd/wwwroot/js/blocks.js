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
const eyeIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eye" viewBox="0 0 16 16">
                  <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z"/>
                  <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"/>
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
    mainDiv.className = "col-11 pointContainer";

    let pointDiv = document.createElement("div");
    pointDiv.className = "point";
    pointDiv.style.backgroundColor = point.BackgroundColor;
    pointDiv.addEventListener("click", function (e) { ChangePointFocus(this, point.IsBranch) });

    let title = document.createElement("input");
    title.className = "pointTitle";
    title.value = point.Title;

    let addedAttrsDiv = document.createElement("div");
    addedAttrsDiv.className = "addedAttributes";

    let btnAddAttr = document.createElement("button");
    btnAddAttr.className = "btn btn-outline-dark tinyButton";
    btnAddAttr.innerHTML = plusIcon + "atrybut";
    if (!point.IsBranch) {
        btnAddAttr.addEventListener("click", function (e) {
            Attribute.prototype.AddAttribute(
                Point.prototype.FindPointInPoints(this.parentElement.parentElement.parentElement)
            );
            UpdateAllBranchesOffset();
        });
    }
    else {
        btnAddAttr.addEventListener("click", function (e) {
            Attribute.prototype.AddAttribute(
                Branch.prototype.FindBranchPoint(this.parentElement.parentElement.parentElement)
            );
            UpdateAllBranchesOffset();
        });
    }
    

    let btnAddLinkAttr = document.createElement("button");
    btnAddLinkAttr.className = "btn btn-outline-dark tinyButton";
    btnAddLinkAttr.innerHTML = plusIcon + "link";
    btnAddLinkAttr.addEventListener("click", function (e) { OpenAddLinkBox(this) })

    let addAttrDiv = document.createElement("div");
    addAttrDiv.className = "btn-group";

    addAttrDiv.appendChild(btnAddAttr);
    addAttrDiv.appendChild(btnAddLinkAttr);

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
    let btnBgColor = document.createElement("input");

    btnUp.title = "Przenieś wyżej";
    btnUp.className = "btn btn-outline-secondary tinyButton";
    if (!point.IsBranch) {
        btnUp.addEventListener("click", function (e) { Point.prototype.MovePoint(this.parentElement.parentElement.parentElement, "up") });
    } else {
        btnUp.addEventListener("click", function (e) { Point.prototype.MoveBranchPoint(this.parentElement.parentElement.parentElement, "up") });
    }
    
    btnUp.innerHTML = upIcon;

    btnDown.title = "Przenieś niżej";
    btnDown.className = "btn btn-outline-secondary tinyButton";
    if (!point.IsBranch) {
        btnDown.addEventListener("click", function (e) { Point.prototype.MovePoint(this.parentElement.parentElement.parentElement, "down") });
    } else {
        btnDown.addEventListener("click", function (e) { Point.prototype.MoveBranchPoint(this.parentElement.parentElement.parentElement, "down") });
    }
    
    btnDown.innerHTML = downIcon;

    btnDelete.title = "Usuń punkt";
    btnDelete.className = "btn btn-outline-danger tinyButton";
    btnDelete.addEventListener("click", function (e) {
        let type = (point.IsBranch) ? "branch-point" : "point";
        OpenRemoveElQuestionBox(this.parentElement.parentElement.parentElement, type);
    })
    btnDelete.innerHTML = crossIcon;

    btnBgColor.title = "Kolor punktu";
    btnBgColor.value = point.BackgroundColor === "white" ? "#ffffff" : RGBToHex(point.BackgroundColor);
    btnBgColor.className = "btn btn-outline-secondary tinyButton";
    btnBgColor.type = "color";
    btnBgColor.style.width = "28px";
    btnBgColor.style.height = "28px";
    btnBgColor.style.padding = "0";
    btnBgColor.addEventListener("change", function (e) { ColorPick(this)})
    btnBgColor.addEventListener("input", function (e) { ColorPick(this)})
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
        btnAddBranch.className = "btn btn-outline-secondary tinyButton addBranchBtn";
        btnAddBranch.innerHTML = plusIcon + "dodaj gałąź";
        btnAddBranch.addEventListener("click", function (e) {
            Branch.prototype.AddBranch(this.parentElement.parentElement);
            AddAndRemoveBranchVisibility(this);
            ChangeBranchFocus(this.parentElement.parentElement);
        });

        branchManageBtns.appendChild(btnAddBranch);
    }

    let visibilityAndRemoveBtns = document.createElement("div");
    visibilityAndRemoveBtns.className = "btn-group visibAndRemoveBtns";
    visibilityAndRemoveBtns.style.visibility = "hidden";
    visibilityAndRemoveBtns.style.position = "absolute";
    visibilityAndRemoveBtns.style.right = "0";

    let btnBranchVisibility = document.createElement("button");
    btnBranchVisibility.title = "Ukryj/pokaż gałąź";
    btnBranchVisibility.className = "btn btn-outline-secondary tinyButton";
    btnBranchVisibility.innerHTML = eyeIcon;
    btnBranchVisibility.addEventListener("click", function (e) { ChangeBranchFocus(this.parentElement.parentElement.parentElement) });

    let btnDeleteBranch = document.createElement("button");
    btnDeleteBranch.title = "Usuń gałąź";
    btnDeleteBranch.className = "btn btn-outline-danger tinyButton";
    btnDeleteBranch.innerHTML = crossIcon;
    btnDeleteBranch.addEventListener("click", function (e) {
        OpenRemoveElQuestionBox(this.parentElement.parentElement.parentElement, "branch");
        //Branch.prototype.DeleteBranch(this.parentElement.parentElement.parentElement);
        AddAndRemoveBranchVisibility(this.parentElement)
    });

    visibilityAndRemoveBtns.appendChild(btnBranchVisibility);
    visibilityAndRemoveBtns.appendChild(btnDeleteBranch);

    branchManageBtns.appendChild(visibilityAndRemoveBtns);

    mainDiv.appendChild(branchManageBtns);

    return mainDiv;
}

function AttributeBlock(attribute) {
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
    keyInput.className = "pointAttrInput attrKey";
    keyInput.value = attribute.Key;
    keyInput.addEventListener("input", function (e) { ResizeInput(e.target) });
    keyInput.style.width = "8ch";

    inputDiv.appendChild(keyInput);
    inputDiv.appendChild(document.createTextNode(": "));

    if (attribute.Type === "attr") {
        let valueInput = document.createElement("input");
        valueInput.className = "pointAttrInput attrValue";
        valueInput.value = "wartość";
        valueInput.addEventListener("input", function (e) { ResizeInput(e.target) });
        valueInput.style.width = "8ch";
        inputDiv.appendChild(valueInput);
    }
    else if (attribute.Type === "link") {
        let linkField = document.createElement("a");
        linkField.href = attribute.HiddenVal;
        linkField.target = "_blank";
        if (attribute.Value != "") {
            linkField.textContent = attribute.Value;
        }

        inputDiv.appendChild(linkField);
    }
    else if (attribute.Type === "map-link") {
        let linkField = document.createElement("a");
        linkField.href = "#";
        linkField.textContent = attribute.Value;
        linkField.addEventListener("click", function (e) {
            getPointByEntityId(attribute.HiddenVal);
        });

        inputDiv.appendChild(linkField);
    }
    
    let hiddenDiv = document.createElement("div");
    hiddenDiv.style.visibility = "hidden";
    hiddenDiv.style.position = "absolute";

    inputDiv.appendChild(hiddenDiv);

    //--------------------------------------------------------

    let btnsDiv = document.createElement("div");
    btnsDiv.className = "col justify-content-end attrBtns";
    btnsDiv.style.visibility = "hidden";

    let btnsGroup = document.createElement("div")
    btnsGroup.className = "btn-group";

    let btnUp = document.createElement("button");
    btnUp.type = "button";
    btnUp.className = "btn btn-outline-secondary attrBtn tinyButton";
    btnUp.addEventListener("click", function (e) { Attribute.prototype.MoveAttribute(this.parentElement.parentElement.parentElement, "up") });
    btnUp.innerHTML = upIcon;

    let btnDown = document.createElement("button");
    btnDown.type = "button";
    btnDown.className = "btn btn-outline-secondary attrBtn tinyButton";
    btnDown.addEventListener("click", function (e) { Attribute.prototype.MoveAttribute(this.parentElement.parentElement.parentElement, "down") });
    btnDown.innerHTML = downIcon;

    let btnCross = document.createElement("button");
    btnCross.type = "button";
    btnCross.className = "btn btn-outline-danger attrBtn tinyButton";
    btnCross.addEventListener("click", function (e) {
        OpenRemoveElQuestionBox(this.parentElement.parentElement.parentElement, "attribute");
        //Attribute.prototype.RemoveAttribute(this.parentElement.parentElement.parentElement)
    });
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

    let lineDiv = document.createElement("div");
    lineDiv.className = "branchLine";

    let pointsDiv = document.createElement("div");
    pointsDiv.className = "addedPoints";

    let addPointDiv = document.createElement("div");
    addPointDiv.className = "col-8 point addPoint";
    addPointDiv.addEventListener("click", function (e) { Point.prototype.AddBranchPoint(this) })

    let p = document.createElement("p");
    p.textContent = "+ Dodaj punkt";

    //pointsDiv.appendChild(PointBlock());

    addPointDiv.appendChild(p);

    branchDiv.appendChild(lineDiv);
    branchDiv.appendChild(pointsDiv);
    branchDiv.appendChild(addPointDiv);

    return branchDiv;
}

function RemoveElQuestionBlock(sender, type) {
    let windowDiv = document.createElement("div");
    windowDiv.className = "windowDiv";
    windowDiv.id = "removeQuestion";

    let centerBoxPos = document.createElement("div");
    centerBoxPos.className = "centerBoxPosition";

    let centerBox = document.createElement("div");
    centerBox.className = "centerBox";

    let container = document.createElement("div");
    container.className = "container";

    let questionDiv = document.createElement("div");
    questionDiv.className = "row addLinkField";

    let text = document.createElement("label");
    text.textContent = "Czy na pewno chcesz usunąć ten element ?";

    let btnsContainer = document.createElement("div");
    btnsContainer.className = "row justify-content-between";

    let btnCancel = document.createElement("button");
    btnCancel.className = "col-4 btn btn-outline-danger";
    btnCancel.id = "closeQuestionBox";
    btnCancel.textContent = "Anuluj";
    btnCancel.addEventListener("click", function (e) {
        document.getElementById("removeQuestion").remove();
    });

    let btnDelete = document.createElement("button");
    btnDelete.className = "col-4 btn btn-outline-dark";
    btnDelete.id = "removeEl";
    btnDelete.textContent = "Usuń";
    btnDelete.addEventListener("click", function (e) {
        RemoveByType(sender, type);
        document.getElementById("removeQuestion").remove();
    });

    btnsContainer.appendChild(btnCancel);
    btnsContainer.appendChild(btnDelete);

    questionDiv.appendChild(text);

    container.appendChild(questionDiv);
    container.appendChild(btnsContainer);

    centerBox.appendChild(container);

    centerBoxPos.appendChild(centerBox);

    windowDiv.appendChild(centerBoxPos);

    return windowDiv;
}