
function getActivePageLink() {
    return document.getElementsByClassName('nav-link active')[0];
}

function getActivePageContent() {
    return document.getElementsByClassName('tab-pane fade show active')[0];
}

function createTextEditor(id) {
    const element = document.getElementById("editor" + id);

    ClassicEditor
        .create(element,
            {
                toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote'],
                heading:
                {
                    options: [
                        { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                        { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                        { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' }
                    ]
                }
            })
        .catch(error => {
            console.log(error);
        });
}

function changePageName() {
    const pageTab = getActivePageLink();
    const pageName = document.getElementById("pageName" + pageTab.id).value;
    pageTab.innerHTML = pageName;
}

function addPage() {
    const elem = document.getElementById("pageTabs");
    const pageId = uuidv4();

    const li = document.createElement("li");
    li.setAttribute("class", "nav-item");
    li.setAttribute("id", "li" + pageId);

    const a = document.createElement("a");
    a.setAttribute("href", "#View" + pageId);
    a.setAttribute("id", pageId);
    a.setAttribute("class", "nav-link active");
    a.setAttribute("data-toggle", "tab");
    a.setAttribute("role", "tab");
    a.setAttribute("aria-selected", "true");
    const newPageNumber = $('#pageTabs li').length + 1;
    a.innerHTML = "Page" + newPageNumber;

    li.append(a);

    setActivePageNotActive();
    elem.append(li);

    setActivePageContentNotActive();
    addContentBlockForPage(pageId);
    addEditBlockForPage(pageId, newPageNumber);
    updateQuestionsAndPagesCount();
    updatePageNumbersBlock();
}

function setActivePageNotActive() {
    const elem = getActivePageLink();
    if (elem != undefined) {
        elem.classList.remove("active");
    }
}

function setActivePageContentNotActive() {
    const elem = getActivePageContent();
    if (elem != undefined) {
        elem.classList.remove("active");
    }
}

function addContentBlockForPage(pageId) {
    const div = document.createElement("div");
    div.setAttribute("class", "tab-pane fade show active");
    div.setAttribute("role", "tabpanel");
    div.setAttribute("id", "View" + pageId);

    document.getElementsByClassName('tab-content')[0].append(div);
}

function addEditBlockForPage(pageId, pageNumber) {
    const page = getActivePageContent();
    const editBlock = createEditBlockForPage(pageId, pageNumber);
    page.append(editBlock);
}

function createEditBlockForPage(pageId, pageNumber) {
    const editBlock = document.createElement("div");
    editBlock.setAttribute("class", "edit-page-block");

    const editNamePageBlock = document.createElement("div");
    editNamePageBlock.setAttribute("class", "edit-name-page");

    const input = document.createElement("input");
    input.setAttribute("class", "form-control");
    input.setAttribute("type", "text");
    input.setAttribute("id", "pageName" + pageId);
    input.setAttribute("value", "Page" + pageNumber);
    input.addEventListener("input", e => {
        e.preventDefault();
        changePageName();
    });

    editNamePageBlock.append(input);

    const deletePageBlock = document.createElement("div");
    deletePageBlock.setAttribute("class", "edit-page-icon");

    const deletePageButton = document.createElement("button");
    deletePageButton.setAttribute("class", "btn btn-danger btn-sm");
    deletePageButton.addEventListener("click", e => {
        e.preventDefault();
        deletePage();
    });

    const deleteIcon = document.createElement("i");
    deleteIcon.setAttribute("class", "bi bi-trash");

    deletePageButton.append(deleteIcon);
    deletePageBlock.append(deletePageButton);

    editBlock.append(editNamePageBlock);
    editBlock.append(deletePageBlock);

    return editBlock;
}

function deletePage() {
    const page = getActivePageLink();
    const tabLink = document.getElementById("li" + page.id);
    const tabContent = getActivePageContent();
    tabLink.remove();
    tabContent.remove();

    updateQuestionsAndPagesCount();
    updatePageNumbersBlock();
    if (isPageTabsExists()) {
        setNotActivePageContentActive();
        setNotActivePageActive();
    }
}

function isPageTabsExists() {
    const pageTabs = document.getElementById("pageTabs").childNodes;
    if (pageTabs && pageTabs.length >= 1) {
        return true;
    }

    return false;
}

function setNotActivePageActive() {
    const links = document.getElementById('pageTabs');

    const link = links.childNodes[links.childElementCount - 1].childNodes[0];
    if (link != undefined) {
        link.classList.add("active");
    }
}

function setNotActivePageContentActive() {
    const elem = document.getElementsByClassName('tab-pane fade');
    if (elem[elem.length - 1] != undefined) {
        elem[elem.length - 1].classList.add("show");
        elem[elem.length - 1].classList.add("active");
    }
}


function addQuestionWithTextEditor() {
    addPageIfNotExist();

    const page = getActivePageContent();

    const id = uuidv4();
    const description = "Question with text editor?";

    const question = createQuestionCard(id, false, createQuestionWithTextEditor(id, description, false, false));

    page.append(question);

    createTextEditor(id);
    updateQuestionDescription(id);
    updateQuestionsAndPagesCount();
}

function addPageIfNotExist() {
    if (!isPageTabsExists()) {
        addPage();
    }
}

function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g,
        c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

function createQuestionWithTextEditor(id, description, isRequired, isEditing) {
    const questionBlock = createCardBodyBlock();

    if (isEditing) {
        questionBlock.append(createHeaderEditingQuestion(id, description, isRequired));
        questionBlock.append(createTextArea(id));
        questionBlock.append(createFooterEditingQuestion(id, description, isRequired, "text"));
    } else {
        questionBlock.append(createQuestionDescription(id, description, isRequired));
        questionBlock.append(createQuestionEditLink(id, description, isRequired, "text"));
        questionBlock.append(createTextArea(id));
    }

    return questionBlock;
}

function createCardBodyBlock() {
    const cardBody = document.createElement("div");
    cardBody.setAttribute("class", "card-body");

    return cardBody;
}

function createTextArea(id) {
    const textAreaBlock = document.createElement("div");
    textAreaBlock.setAttribute("class", "textarea-block");

    const textarea = document.createElement("textarea");
    textarea.setAttribute("name", "editor");
    textarea.setAttribute("id", "editor" + id);

    textAreaBlock.append(textarea);

    return textAreaBlock;
}

function addQuestionWithScale() {
    addPageIfNotExist();
    const page = getActivePageContent();

    const id = uuidv4();
    const description = "Question with scale?";

    const question = createQuestionCard(id, false, createQuestionWithScale(id, description, false, false));

    page.append(question);
    updateQuestionDescription(id);
    updateQuestionsAndPagesCount();
}

function createQuestionWithScale(id, description, isRequired, isEditing) {
    const questionBlock = createCardBodyBlock();

    if (isEditing) {
        questionBlock.append(createHeaderEditingQuestion(id, description, isRequired));
        questionBlock.append(createScaleInputBlock());
        questionBlock.append(createScaleOutputBlock());
        questionBlock.append(createFooterEditingQuestion(id, description, isRequired, "scale"));
    } else {
        questionBlock.append(createQuestionDescription(id, description, isRequired));
        questionBlock.append(createQuestionEditLink(id, description, isRequired, "scale"));
        questionBlock.append(createScaleInputBlock());
        questionBlock.append(createScaleOutputBlock());
    }

    return questionBlock;
}

function createScaleInputBlock() {
    const input = document.createElement("input");
    input.setAttribute("type", "range");
    input.setAttribute("value", "50");
    input.setAttribute("min", "1");
    input.setAttribute("max", "100");
    input.setAttribute("class", "scale-block");
    input.setAttribute("oninput", "this.nextElementSibling.value = this.value");

    return input;
}

function createScaleOutputBlock(parameters) {
    const output = document.createElement("output");
    output.append("50");

    return output;
}

function addQuestionWithRating() {
    addPageIfNotExist();
    const page = getActivePageContent();

    const id = uuidv4();
    const description = "Question with rating ?";

    const question = createQuestionCard(id, false, createQuestionWithRating(id, description, false, false));

    page.append(question);
    updateQuestionDescription(id);
    updateQuestionsAndPagesCount();
}

function createQuestionWithRating(id, description, isRequired, isEditing) {
    const questionBlock = createCardBodyBlock();

    if (isEditing) {
        questionBlock.append(createHeaderEditingQuestion(id, description, isRequired));
        questionBlock.append(createRatingBlock(id));
        questionBlock.append(createFooterEditingQuestion(id, description, isRequired, "rating"));

    } else {
        questionBlock.append(createQuestionDescription(id, description, isRequired));
        questionBlock.append(createQuestionEditLink(id, description, isRequired, "rating"));
        questionBlock.append(createRatingBlock(id));
    }

    return questionBlock;
}

function createRatingBlock(id) {
    const ratingBlock = document.createElement("div");
    ratingBlock.setAttribute("class", "rating");

    for (let i = 5; i >= 0; i--) {
        const input = document.createElement("input");
        input.setAttribute("type", "radio");
        input.setAttribute("name", "rating " + id);
        input.setAttribute("value", i);
        input.setAttribute("id", id + i);

        const label = document.createElement("label");
        label.setAttribute("for", id + i);

        label.append("☆");

        ratingBlock.append(input);
        ratingBlock.append(label);
    }

    return ratingBlock;
}

function addQuestionWithFileInput() {
    addPageIfNotExist();
    const page = getActivePageContent();

    const id = uuidv4();
    const description = "Question with file input ?";

    const question = createQuestionCard(id, false, createQuestionWithFileInput(id, description, false, false));

    page.append(question);
    updateQuestionDescription(id);
    updateQuestionsAndPagesCount();
}

function createQuestionWithFileInput(id, description, isRequired, isEditing) {
    const questionBlock = createCardBodyBlock();

    if (isEditing) {
        questionBlock.append(createHeaderEditingQuestion(id, description, isRequired));
        questionBlock.append(createFileInputBlock(id));
        questionBlock.append(createFooterEditingQuestion(id, description, isRequired, "file"));
    } else {
        questionBlock.append(createQuestionDescription(id, description, isRequired));
        questionBlock.append(createQuestionEditLink(id, description, isRequired, "file"));
        questionBlock.append(createFileInputBlock(id));
    }

    return questionBlock;
}

function createFileInputBlock(id) {
    const inputBlock = document.createElement("div");
    inputBlock.setAttribute("class", "form-group");

    const input = document.createElement("input");
    input.setAttribute("type", "file");
    input.setAttribute("class", "form-control-file");
    input.setAttribute("id", "formControlFile" + id);

    inputBlock.append(input);

    return inputBlock;
}

function addQuestionWithAnswers(type) {
    addPageIfNotExist();
    const page = getActivePageContent();

    const id = uuidv4();
    const description = "Question with many answers ?";
    const answers = ["first", "second", "third"];

    const question = createQuestionCard(id, false, createQuestionWithAnswers(id, description, false, answers, type, false));

    page.append(question);
    updateQuestionDescription(id);
    updateQuestionsAndPagesCount();
}

function createQuestionWithAnswers(id, description, isRequired, answers, type, isEditing) {
    const questionBlock = createCardBodyBlock();

    if (isEditing) {
        questionBlock.append(createHeaderEditingQuestion(id, description, isRequired));
        questionBlock.append(createAnswersBlock(id, answers, type, true));
        questionBlock.append(createFooterEditingQuestion(id, description, isRequired, type, answers));
    } else {
        questionBlock.append(createQuestionDescription(id, description, isRequired));
        questionBlock.append(createQuestionEditLink(id, description, isRequired, type, answers));
        questionBlock.append(createAnswersBlock(id, answers, type, false));
    }

    return questionBlock;
}

function createAnswersBlock(id, answers, type, isEditing) {
    const mainBlock = document.createElement("div");

    answers.forEach(function (answer) {
        const answerBlock = document.createElement("div");
        answerBlock.setAttribute("class", "form-check");

        const input = document.createElement("input");
        input.setAttribute("class", "form-check-input");
        input.setAttribute("type", type);
        input.setAttribute("id", id + answer);

        const label = document.createElement("label");
        label.setAttribute("class", "form-check-label");
        label.setAttribute("for", id + answer);

        if (isEditing) {
            const inputInLabel = document.createElement("input");
            inputInLabel.setAttribute("class", "form-control");
            inputInLabel.setAttribute("value", answer);
            inputInLabel.setAttribute("name", "answers" + id);

            label.append(inputInLabel);
        } else {
            label.append(answer);
        }

        answerBlock.append(input);
        answerBlock.append(label);

        mainBlock.append(answerBlock);
    });

    return mainBlock;
}

function createQuestionDescription(id, description, isRequired) {
    const descriptionBlock = document.createElement("div");
    descriptionBlock.setAttribute("class", "question-block");

    descriptionBlock.append(createHiddenDescriptionInput(id, description));
    descriptionBlock.append(createHiddenIsRequiredValueInput(id, isRequired));

    const text = document.createElement("h5");
    text.setAttribute("id", "descriptionHeader" + id);
    text.append(description);

    descriptionBlock.append(text);

    return descriptionBlock;
}

function createHiddenDescriptionInput(id, description) {
    const hiddenInput = document.createElement("input");
    hiddenInput.setAttribute("type", "hidden");
    hiddenInput.setAttribute("value", description);
    hiddenInput.setAttribute("id", "initDescription" + id);

    return hiddenInput;
}

function createHiddenIsRequiredValueInput(id, isRequired) {
    const hiddenInput = document.createElement("input");
    hiddenInput.setAttribute("type", "hidden");
    hiddenInput.setAttribute("id", "isRequiredValue" + id);
    hiddenInput.checked = isRequired;

    return hiddenInput;
}

function createQuestionEditLink(id, description, isRequired, type, answers) {
    const editBlock = document.createElement("div");
    editBlock.setAttribute("class", "edit-question-icon");

    const editLink = document.createElement("a");
    editLink.setAttribute("class", "btn btn-link");
    editLink.setAttribute("type", "button");
    editLink.addEventListener("click", e => {
        e.preventDefault();
        changeQuestionMode(id, description, isRequired, true, type, answers);
        updateQuestionDescription(id);
    });

    const editIcon = document.createElement("i");
    editIcon.setAttribute("class", "bi bi-pencil-fill");

    editLink.append(editIcon);
    editBlock.append(editLink);

    return editBlock;
}

function createQuestionCard(id, isEditingMode, questionBlock) {
    const mainBlock = document.createElement("div");
    mainBlock.setAttribute("class", "main-question-card");
    mainBlock.setAttribute("id", id);

    const questionCard = document.createElement("div");
    questionCard.setAttribute("class", "card");
    if (isEditingMode) {
        questionCard.setAttribute("style", "width: 37.5rem; border: 1px solid #dfe3e7;");
    } else {
        questionCard.setAttribute("style", "width: 37.5rem; border: 0;");
    }

    questionCard.append(questionBlock);
    mainBlock.append(questionCard);

    return mainBlock;
}

function createHeaderEditingQuestion(id, description, isRequired) {
    const header = document.createElement("div");

    const questionSettings = document.createElement("div");
    questionSettings.setAttribute("class", "edit-question-block-header");

    questionSettings.append(createNavigationBlock(id));
    questionSettings.append(createBlockForCheckQuestionRequire(id, isRequired));
    questionSettings.append(createBlockForDeleteQuestion(id));

    const editingDescriptionBlock = createBlockForEditingDescription(id, description);
    header.append(questionSettings);
    header.append(editingDescriptionBlock);

    return header;
}

function createBlockForEditingDescription(id, description) {
    const mainBlock = document.createElement("div");
    mainBlock.setAttribute("class", "edit-question-block");

    const blockForQuestionNumber = document.createElement("div");
    blockForQuestionNumber.setAttribute("class", "question-edit-block");

    blockForQuestionNumber.append(createHiddenDescriptionInput(id, ""));

    const questionNumber = document.createElement("h5");
    questionNumber.setAttribute("id", "descriptionHeader" + id);

    blockForQuestionNumber.append(questionNumber);

    const blockForEditingDescription = document.createElement("div");
    blockForEditingDescription.setAttribute("class", "edit-question-description");

    const input = document.createElement("input");
    input.setAttribute("class", "form-control");
    input.setAttribute("id", "descriptionInput" + id);
    input.setAttribute("value", description);

    blockForEditingDescription.append(input);

    mainBlock.append(blockForQuestionNumber);
    mainBlock.append(blockForEditingDescription);

    return mainBlock;
}

function createNavigationBlock(id) {
    const navigationBlock = document.createElement("div");
    navigationBlock.setAttribute("class", "edit-question-block-main-header");

    const arrowDownLink = createLinkForQuestionNavigation();
    const iconArrowDown = document.createElement("i");
    iconArrowDown.setAttribute("class", "bi bi-arrow-down");

    arrowDownLink.append(iconArrowDown);
    arrowDownLink.addEventListener("click", e => {
        e.preventDefault();
        moveQuestionDown(id);
        updateQuestionsDescription();
    });

    const arrowUpLinkBlock = document.createElement("div");
    arrowUpLinkBlock.setAttribute("class", "question-move-block");

    const arrowUpLink = createLinkForQuestionNavigation();
    const iconArrowUp = document.createElement("i");
    iconArrowUp.setAttribute("class", "bi bi-arrow-up");

    arrowUpLink.append(iconArrowUp);
    arrowUpLink.addEventListener("click", e => {
        e.preventDefault();
        moveQuestionUp(id);
        updateQuestionsDescription();
    });

    arrowUpLinkBlock.append(arrowUpLink);

    navigationBlock.append(arrowDownLink);
    navigationBlock.append(arrowUpLinkBlock);

    return navigationBlock;
}

function createBlockForCheckQuestionRequire(id, isRequired) {
    const mainBlock = document.createElement("div");
    mainBlock.setAttribute("class", "question-checkbox-block");

    const checkboxBlock = document.createElement("div");
    checkboxBlock.setAttribute("class", "form-check");

    const input = document.createElement("input");
    input.setAttribute("class", "form-check-input");
    input.setAttribute("type", "checkbox");
    input.setAttribute("id", "isRequired" + id);
    input.checked = isRequired;

    const label = document.createElement("label");
    label.setAttribute("class", "form-check-label");
    label.setAttribute("for", "isRequired" + id);
    label.append("Required");

    checkboxBlock.append(input);
    checkboxBlock.append(label);

    mainBlock.append(checkboxBlock);

    return mainBlock;
}

function createBlockForDeleteQuestion(id) {
    const deleteQuestionBlock = document.createElement("div");
    deleteQuestionBlock.setAttribute("class", "edit-question-icon");

    const deleteQuestionButton = document.createElement("button");
    deleteQuestionButton.setAttribute("class", "btn btn-danger btn-sm");
    deleteQuestionButton.addEventListener("click", e => {
        e.preventDefault();
        deleteQuestion(id);
        updateQuestionsDescription();
        updateQuestionsAndPagesCount()
    });

    const deleteQuestionIcon = document.createElement("i");
    deleteQuestionIcon.setAttribute("class", "bi bi-trash");

    deleteQuestionButton.append(deleteQuestionIcon);
    deleteQuestionBlock.append(deleteQuestionButton);

    return deleteQuestionBlock;
}

function createLinkForQuestionNavigation() {
    const arrowLink = document.createElement("a");
    arrowLink.setAttribute("class", "btn btn-light btn-sm");
    arrowLink.setAttribute("style", "background-color: white; border: 0");

    return arrowLink;
}

function createFooterEditingQuestion(id, description, isRequired, type, answers) {
    const footer = document.createElement("div");
    footer.setAttribute("class", "question-buttons-block");

    const saveButtonBlock = document.createElement("div");
    saveButtonBlock.setAttribute("class", "question-button-save");

    const saveButton = document.createElement("button");
    saveButton.setAttribute("class", "btn btn-primary");
    saveButton.append("Save");
    saveButton.addEventListener("click", e => {
        e.preventDefault();
        changeQuestionMode(id, getNewDescription(id), getNewIsRequiredValue(id), false, type, answers == undefined ? undefined : getNewAnswers(id));
        updateQuestionDescription(id);
    });

    saveButtonBlock.append(saveButton);

    const cancelButton = document.createElement("button");
    cancelButton.setAttribute("class", "btn btn-secondary");
    cancelButton.append("Cancel");
    cancelButton.addEventListener("click", e => {
        e.preventDefault();
        changeQuestionMode(id, description, isRequired, false, type, answers);
        updateQuestionDescription(id);
    });

    footer.append(saveButtonBlock);
    footer.append(cancelButton);

    return footer;
}

function changeQuestionMode(id, description, isRequired, isEditing, type, answers) {
    const question = document.getElementById(id);
    let editQuestion = question;

    switch (type) {
        case "radio":
            {
                editQuestion = createQuestionWithAnswers(id, description, isRequired, answers, type, isEditing);
                break;
            }
        case "checkbox":
            {
                editQuestion = createQuestionWithAnswers(id, description, isRequired, answers, type, isEditing);
                break;
            }
        case "file":
            {
                editQuestion = createQuestionWithFileInput(id, description, isRequired, isEditing);
                break;
            }
        case "scale":
            {
                editQuestion = createQuestionWithScale(id, description, isRequired, isEditing);
                break;
            }
        case "rating":
            {
                editQuestion = createQuestionWithRating(id, description, isRequired, isEditing);
                break;
            }
        case "text":
            {
                editQuestion = createQuestionWithTextEditor(id, description, isRequired, isEditing);
                break;
            }
    }

    if (question !== editQuestion) {
        question.after(createQuestionCard(id, isEditing, editQuestion));
        question.remove();

        if (type === "text") {
            createTextEditor(id);
        }
    }
}

function getQuestionNumber(id) {
    const pageContent = getActivePageContent();
    let questionNumber = pageContent.childNodes.length;

    pageContent.childNodes.forEach(function (node, number) {
        if (node.id === id) {
            questionNumber = number;
        }
    });

    return questionNumber;
}

function getNewDescription(id) {
    return document.getElementById("descriptionInput" + id).value;
}

function getNewIsRequiredValue(id) {
    return document.getElementById("isRequired" + id).checked;
}

function getNewAnswers(id) {
    const newAnswers = [];
    const inputs = (document.getElementsByName("answers" + id));
    inputs.forEach(function (input) {
        newAnswers.push(input.value);
    });

    return newAnswers;
}

function deleteQuestion(id) {
    const question = document.getElementById(id);
    question.remove();
}

function moveQuestionDown(id) {
    const pageContent = getActivePageContent();
    const questionIndex = getIndexOfChildNodeById(pageContent.childNodes, id);

    if (questionIndex < 1 || questionIndex >= pageContent.childNodes.length - 1) {
        return;
    }

    swapQuestions(pageContent, questionIndex + 1, questionIndex);
}

function moveQuestionUp(id) {
    const pageContent = getActivePageContent();
    const questionIndex = getIndexOfChildNodeById(pageContent.childNodes, id);

    if (questionIndex <= 1) {
        return;
    }

    swapQuestions(pageContent, questionIndex, questionIndex - 1);
}

function getIndexOfChildNodeById(childNodes, id) {
    let questionIndex = -1;
    childNodes.forEach(function (value, index) {
        if (value.id === id) {
            questionIndex = index;
        }
    });

    return questionIndex;
}

function swapQuestions(pageContent, indexA, indexB) {
    const replacedNode = pageContent.replaceChild(pageContent.childNodes[indexA], pageContent.childNodes[indexB]);
    pageContent.insertBefore(replacedNode, pageContent.childNodes[indexB].nextSibling);
}

function updateQuestionsDescription() {
    const pageContentChildNodes = getActivePageContent().childNodes;
    const questionsId = [];
    for (let i = 1; i < pageContentChildNodes.length; i++) {
        questionsId.push(pageContentChildNodes[i].id);
    }

    questionsId.forEach(function (id) {
        updateQuestionDescription(id);
    });
}

function updateQuestionDescription(id) {
    let description = "";
    if (isQuestionHaveNumber()) {
        description += getQuestionNumber(id);
        description += ". ";
    }

    if (isRequiredFieldsAsterisks()) {
        const isRequiredInput = document.getElementById("isRequiredValue" + id);
        if (isRequiredInput && isRequiredInput.checked) {
            description += "*";
        }
    }

    description += document.getElementById("initDescription" + id).value;
    document.getElementById("descriptionHeader" + id).innerHTML = description;
}

function isQuestionHaveNumber() {
    return document.getElementById("questionNumbers").checked;
}

function isRequiredFieldsAsterisks() {
    return document.getElementById("requiredFieldsAsterisks").checked;
}

function isRequiredProgressBar() {
    return document.getElementById("fieldProgressBar").checked;
}

function updateProgressBarBlock() {
    const isRequiredProgressBarBlock = isRequiredProgressBar();
    const progressBarBlock = document.getElementsByClassName("progress-bar-block");

    if (progressBarBlock.length === 0 && isRequiredProgressBarBlock) {
        document.getElementsByClassName("new-survey-block")[0].append(createProgressBarBlock());
        updatePageNumbersBlock();
    } else if (progressBarBlock !== 0 && !isRequiredProgressBarBlock) {
        progressBarBlock[0].remove();
    }
}

function updatePageNumbersBlock() {
    const isRequiredPageNumbersBlock = isRequiredPageNumbers();
    const progressBlock = document.getElementsByClassName("progress-block");
    const pageNumbersBlocks = document.getElementsByClassName("pages-near-progress-bar");

    if (isRequiredPageNumbersBlock && progressBlock.length !== 0 && pageNumbersBlocks.length === 0) {
        document.getElementsByClassName("progress-bar-block")[0].insertBefore(createPageNumbersBlock(), progressBlock[0]);
    } else if (isRequiredPageNumbersBlock && progressBlock.length !== 0 && pageNumbersBlocks.length !== 0) {
        pageNumbersBlocks[0].remove();
        document.getElementsByClassName("progress-bar-block")[0].insertBefore(createPageNumbersBlock(), progressBlock[0]);
    } else if (!isRequiredPageNumbersBlock && pageNumbersBlocks.length !== 0) {
        pageNumbersBlocks[0].remove();
    }
}

function createPageNumbersBlock() {
    const pageNumbersBlock = document.createElement("div");
    pageNumbersBlock.setAttribute("class", "pages-near-progress-bar");

    let pagesInfo = "0/";
    pagesInfo += getPageCount();
    pageNumbersBlock.append(pagesInfo);

    return pageNumbersBlock;
}

function isRequiredPageNumbers() {
    return document.getElementById("pageNumbers").checked;
}

function createProgressBarBlock() {
    const progressBarBlock = document.createElement("div");
    progressBarBlock.setAttribute("class", "progress-bar-block");

    const progressBlock = document.createElement("div");
    progressBlock.setAttribute("class", "progress-block");

    const progress = document.createElement("div");
    progress.setAttribute("class", "progress");

    const progressBar = document.createElement("div");
    progressBar.setAttribute("class", "progress-bar");
    progressBar.setAttribute("role", "progressbar");
    progressBar.setAttribute("style", "width: 25%;");
    progressBar.setAttribute("aria-valuenow", "25");
    progressBar.setAttribute("aria-valuemin", "0");
    progressBar.setAttribute("aria-valuemax", "100");
    progressBar.append("25%");

    progress.append(progressBar);
    progressBlock.append(progress);
    progressBarBlock.append(progressBlock);

    return progressBarBlock;
}

function updateQuestionsAndPagesCount() {
    const questionsAndPagesCountBlock = document.getElementsByClassName("survey-description-block")[0];
    questionsAndPagesCountBlock.innerHTML = "";

    const p = document.createElement("p");
    let info = "Questions: ";
    info += getQuestionCount();
    info += " Pages: ";
    info += getPageCount();
    p.append(info);

    questionsAndPagesCountBlock.append(p);
}

function getQuestionCount() {
    let count = 0;
    const tabContent = document.getElementsByClassName("tab-content")[0];
    tabContent.childNodes.forEach(function (pageContent) {
        count += pageContent.childNodes.length - 1;
    });

    return count;
}

function getPageCount() {
    let count = 0;
    const pageTabs = document.getElementById("pageTabs");
    if (pageTabs) {
        count = pageTabs.childNodes.length;
    }

    return count;
}

document.addEventListener('DOMContentLoaded', function () {
    addPage();
});