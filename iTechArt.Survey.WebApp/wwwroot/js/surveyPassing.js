function changeActivePage(next) {
    const pages = document.getElementsByClassName("tab-pane fade show");
    const activePage = document.getElementsByClassName("tab-pane fade show active")[0];

    if (next === true) {
        for (let i = 0; i < pages.length; i++) {
            if ((pages[i] === activePage) && (i !== pages.length - 1)) {
                activePage.classList.remove("active");
                pages[i + 1].classList.add("active");
            }
        }
    } else {
        for (let i = 0; i < pages.length; i++) {
            if ((pages[i] === activePage) && (i > 0)) {
                activePage.classList.remove("active");
                pages[i - 1].classList.add("active");
            }
        }
    }

    changeButtonsState();
    updateFinishButton();
}

function updateFinishButton() {
    const pages = document.getElementsByClassName("tab-pane fade show");
    const activePage = document.getElementsByClassName("tab-pane fade show active")[0];

    const block = document.getElementsByClassName("save-passing-survey-block")[0];
    block.innerHTML = "";

    if (pages[pages.length - 1] === activePage) {
        const button = document.createElement("button");
        button.setAttribute("id", "savePassingSurveyButton");
        button.setAttribute("class", "btn btn-success btn-block");
        button.innerHTML = "Finish";
        button.addEventListener("click", e => {
            e.preventDefault();
            saveResults();
        });

        block.append(button);
    }
}

function changeButtonsState() {
    const pages = document.getElementsByClassName("tab-pane fade show");
    const activePage = document.getElementsByClassName("tab-pane fade show active")[0];

    const prevPageButton = document.getElementById("prevPageButton");
    const nextPageButton = document.getElementById("nextPageButton");

    prevPageButton.disabled = false;
    nextPageButton.disabled = false;

    if (pages[0] === activePage) {
        prevPageButton.disabled = true;
    } else if (pages[pages.length - 1] === activePage) {
        nextPageButton.disabled = true;
    }
}

function getSurveyId() {
    return document.getElementsByClassName("passing-survey-block")[0].id;
}

async function saveResults() {
    const surveyId = getSurveyId();
    
    const data = {
        surveyId: surveyId,
        userAnswers: getQuestionAnswers(getQuestionsId(), surveyId)
    };

    await $.post("/SurveyPassing/SaveAnswers", $.param(data))
        .then(function () {
            window.location.href = "/";
        })
        .catch(function (response) {
            outputErrors(response.responseJSON);
        });
}

function getQuestionAnswers(questionsId, surveyId) {
    const questionsAnswers = new Array();

    for (let i = 0; i < questionsId.length; i++) {
        const type = document.getElementById("questionType" + questionsId[i]).value;

        let answer = "";
        if (type === "OneAnswer") {
            answer = getAnswerFromQuestionWithOneAnswer(questionsId[i]);
        } else if (type === "ManyAnswers") {
            answer = getAnswerFromQuestionWithManyAnswer(questionsId[i]);
        } else if (type === "Rating") {
            answer = getAnswerFromQuestionWithRating(questionsId[i]);
        } else if (type === "Scale") {
            answer = getAnswerFromQuestionWithScale(questionsId[i]);
        } else if (type === "Text") {
            answer = getAnswerFromQuestionWithText(questionsId[i]);
        } else if (type === "File") {
            answer = getAnswerFromQuestionWithFile(questionsId[i]);
        }

        if (answer === "") {
            continue;
        }

        if (Array.isArray(answer) && answer.length === 0) {
            continue;
        }

        const questionData = {
            "QuestionId": questionsId[i],
            "SurveyId": surveyId,
            "Answer": JSON.stringify(answer)
        };

        questionsAnswers.push(questionData);
    }

    return questionsAnswers;
}

function outputErrors(errors) {
    const questionsId = getQuestionsId();
    for (let i = 0; i < questionsId.length; i++) {
        document.getElementById("errorsList" + questionsId[i]).innerHTML = "";
    }

    for (let i = 0; i < errors.length; i++) {
        const errorList = document.getElementById("errorsList" + errors[i].questionId);
        const li = document.createElement("li");
        li.innerHTML = errors[i].errorMessage;
        errorList.append(li);
    }
}

function getAnswerFromQuestionWithFile(id) {
    const fullPath = document.getElementById("fileInput" + id).value;
    let fileName = "";
    if (fullPath) {
        const startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
        fileName = fullPath.substring(startIndex);
        if (fileName.indexOf('\\') === 0 || fileName.indexOf('/') === 0) {
            fileName = fileName.substring(1);
        }

    }

    return fileName;
}

function getAnswerFromQuestionWithOneAnswer(id) {
    let answer = "";
    const inputs = document.getElementsByName("radioButton" + id);
    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].checked === true) {
            answer = inputs[i].value;
        }
    }

    return answer;
}

function getAnswerFromQuestionWithManyAnswer(id) {
    const answer = [];
    const inputs = document.getElementsByName("checkbox" + id);
    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].checked === true) {
            answer.push(inputs[i].value);
        }
    }

    return answer;
}

function getAnswerFromQuestionWithRating(id) {
    let answer = "";
    const inputs = document.getElementsByName("rating" + id);
    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].checked === true) {
            answer = inputs[i].value;
        }
    }

    return answer;
}

function getAnswerFromQuestionWithScale(id) {
    return document.getElementById("scale" + id).value;
}

function getAnswerFromQuestionWithText(id) {
    const answer = document.getElementById("textArea" + id).nextSibling.childNodes[2].childNodes[0].innerHTML;
    if (answer === "<p><br data-cke-filler=\"true\"></p>") {
        return "";
    }

    return answer;
}

function getQuestionsId() {
    const questionCards = document.getElementsByClassName("card");
    const questionsId = [];

    for (let i = 0; i < questionCards.length; i++) {
        questionsId.push(questionCards[i].id);
    }

    return questionsId;
}

function updateProgress() {
    const pages = document.getElementsByClassName("tab-pane fade show");
    let passedPages = 0;

    for (let i = 0; i < pages.length; i++) {
        const requiredQuestionsId = [];

        for (let j = 1; j < pages[i].children.length; j++) {
            const questionId = pages[i].children[j].children[0].id;

            if (document.getElementById("isRequire" + questionId).value) {
                requiredQuestionsId.push(questionId);
            }
        }

        if (getQuestionAnswers(requiredQuestionsId, getSurveyId()).length === requiredQuestionsId.length) {
            passedPages++;
        }
    }

    updateProgressBarBlock(passedPages, pages.length);
}

function updateProgressBarBlock(passedPagesCount, totalPagesCount) {
    const passedPagesBlock = document.getElementsByClassName("pages-near-progress-bar");
    if (passedPagesBlock.length !== 0) {
        passedPagesBlock[0].innerHTML = passedPagesCount + "/" + totalPagesCount;
    }

    const progressBar = document.getElementsByClassName("progress-bar");
    if (progressBar.length !== 0) {
        if (totalPagesCount !== 0) {
            progressBar[0].setAttribute("style", "width: " + passedPagesCount / totalPagesCount * 100 + "%;");
            progressBar[0].innerHTML = passedPagesCount / totalPagesCount * 100 + "%";
        }
    }
}

export { changeActivePage, updateProgress, saveResults };