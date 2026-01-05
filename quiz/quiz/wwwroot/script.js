let currentQuizId = null;

document.addEventListener("DOMContentLoaded", showList);

// --- NAVIGÁCIÓ ---

function showList() {
    document.getElementById('quiz-list-section').style.display = 'block';
    document.getElementById('create-quiz-section').style.display = 'none';
    document.getElementById('active-quiz-section').style.display = 'none';
    loadQuizzes();
}

function showCreateForm() {
    document.getElementById('quiz-list-section').style.display = 'none';
    document.getElementById('create-quiz-section').style.display = 'block';
    document.getElementById('active-quiz-section').style.display = 'none';
    
    // Űrlap alaphelyzetbe állítása
    document.getElementById('new-quiz-title').value = '';
    document.getElementById('new-questions-container').innerHTML = '';
    addQuestionInput(); // Egy kérdés legyen alapból
}

// --- LISTÁZÁS ---

async function loadQuizzes() {
    const listDiv = document.getElementById('quiz-list');
    
    try {
        const response = await fetch('/Quiz/list');
        
        if (!response.ok) {
            const errorText = await response.text();
            listDiv.innerHTML = `<p style="color:red">Hiba történt a betöltéskor: ${errorText}</p>`;
            return;
        }

        const quizzes = await response.json();
        listDiv.innerHTML = '';

        if (!quizzes || quizzes.length === 0) {
            listDiv.innerHTML = '<p>Nincs elérhető kvíz.</p>';
            return;
        }

        quizzes.forEach(quiz => {
            const btn = document.createElement('button');
            btn.textContent = quiz.title || quiz.Title; 
            btn.className = "btn-primary";
            btn.style.margin = "5px";
            btn.onclick = () => renderQuiz(quiz);
            listDiv.appendChild(btn);
        });
    } catch (error) {
        console.error("Hiba:", error);
        listDiv.innerHTML = '<p style="color:red">Nem sikerült elérni a szervert.</p>';
    }
}

// --- KITÖLTÉS ---

function renderQuiz(quiz) {
    document.getElementById('quiz-list-section').style.display = 'none';
    document.getElementById('active-quiz-section').style.display = 'block';
    
    currentQuizId = quiz.id;
    document.getElementById('quiz-title').textContent = quiz.title || quiz.Title;
    document.getElementById('result').textContent = '';

    const container = document.getElementById('questions-container');
    container.innerHTML = '';

    // A DTO-ból dolgozunk: text, options lista
    quiz.questions.forEach((q, qIndex) => {
        const qDiv = document.createElement('div');
        qDiv.className = 'quiz-card';
        
        const qTitle = document.createElement('div');
        qTitle.className = 'question';
        qTitle.textContent = `${qIndex + 1}. ${q.text || q.Text}`;
        qDiv.appendChild(qTitle);

        const optionsDiv = document.createElement('div');
        optionsDiv.className = 'options';

        q.options.forEach((opt, optIndex) => {
            const label = document.createElement('label');
            
            const radio = document.createElement('input');
            radio.type = 'radio';
            radio.name = `question_${qIndex}`;
            radio.value = optIndex;
            
            label.appendChild(radio);
            label.appendChild(document.createTextNode(` ${opt}`));
            optionsDiv.appendChild(label);
        });
        qDiv.appendChild(optionsDiv);
        container.appendChild(qDiv);
    });
}

async function submitQuiz() {
    if (currentQuizId === null) return;

    const answers = [];
    const container = document.getElementById('questions-container');
    const questionCards = container.querySelectorAll('.quiz-card');

    for (let i = 0; i < questionCards.length; i++) {
        const selected = document.querySelector(`input[name="question_${i}"]:checked`);
        answers.push(selected ? parseInt(selected.value) : -1);
    }

    try {
        const response = await fetch('/Quiz/submit', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ quizId: currentQuizId, selectedAnswerIndices: answers })
        });
        
        if (!response.ok) {
            const errorText = await response.text();
            alert("Hiba: " + errorText);
            return;
        }

        const result = await response.json();
        
        const resultDiv = document.getElementById('result');
        const percentage = Math.round((result.score / result.totalQuestions) * 100);
        resultDiv.textContent = `Eredmény: ${result.score} / ${result.totalQuestions} (${percentage}%)`;
        resultDiv.style.color = result.score === result.totalQuestions ? "green" : "orange";

    } catch (error) { 
        console.error(error);
        alert("Hálózati hiba történt.");
    }
}

// --- ÚJ KVÍZ LÉTREHOZÁSA ---

function addQuestionInput() {
    const container = document.getElementById('new-questions-container');
    const qIndex = container.children.length;

    const card = document.createElement('div');
    card.className = 'quiz-card';
    card.innerHTML = `
        <label><strong>${qIndex + 1}. Kérdés:</strong></label>
        <input type="text" class="input-question-text" style="width: 80%; padding: 5px;" placeholder="Írd be a kérdést...">
        <button onclick="this.parentElement.remove()" class="btn-danger" style="font-size: 0.8em; float: right;">Törlés</button>
        <br><br>
        <label>Válaszlehetőségek (Jelöld be a helyeset):</label>
        <div class="answers-container"></div>
        <button onclick="addAnswerInput(this)" class="btn-secondary" style="font-size: 0.8em; margin-top:5px;">+ Válasz hozzáadása</button>
    `;
    
    container.appendChild(card);
    
    // Alapból adjunk hozzá 2 üres választ
    const answersContainer = card.querySelector('.answers-container');
    addAnswerInputInternal(answersContainer);
    addAnswerInputInternal(answersContainer);
}

function addAnswerInput(btn) {
    addAnswerInputInternal(btn.previousElementSibling);
}

function addAnswerInputInternal(container) {
    // Egyedi csoportnév generálása a kérdéshez, hogy a radio gombok tudják, kivel vannak egy csoportban
    const questionCard = container.closest('.quiz-card');
    // Megkeressük a kérdés kártyán belüli meglévő radio gombokat
    const existingRadios = questionCard.querySelectorAll('input[type="radio"]');
    // Ha már van radio, használjuk annak a nevét, különben generálunk egy újat
    let groupName = existingRadios.length > 0 
        ? existingRadios[0].name 
        : `new_q_group_${Math.random().toString(36).substr(2, 9)}`;

    const div = document.createElement('div');
    div.style.marginBottom = "5px";
    div.style.display = "flex";
    div.style.alignItems = "center";
    
    div.innerHTML = `
        <input type="radio" name="${groupName}" title="Ez a helyes válasz?" style="margin-right: 10px;">
        <input type="text" class="input-answer-text" placeholder="Válasz..." style="width: 60%; margin-right: 10px;">
        <button onclick="this.parentElement.remove()" class="btn-secondary" style="font-size: 0.7em; padding: 2px 8px;">X</button>
    `;
    
    container.appendChild(div);
}

async function saveNewQuiz() {
    const titleInput = document.getElementById('new-quiz-title');
    if (!titleInput.value.trim()) {
        alert("Kérlek add meg a kvíz címét!");
        return;
    }

    // A JSON struktúra építése a TE formátumod (quizzes.json) szerint:
    const quizData = {
        Title: titleInput.value,
        Questions: []
    };

    const questionCards = document.querySelectorAll('#new-questions-container .quiz-card');
    
    for (let card of questionCards) {
        const qText = card.querySelector('.input-question-text').value;
        if (!qText.trim()) continue; // Üres kérdést kihagyunk

        const answerDivs = card.querySelectorAll('.answers-container > div');
        
        const optionsList = []; // Csak a szövegek (string lista)
        let correctIndex = -1;  // Melyik volt bejelölve (int)

        answerDivs.forEach((div, index) => {
            const aText = div.querySelector('.input-answer-text').value;
            const isCorrect = div.querySelector('input[type="radio"]').checked;

            if (aText.trim()) {
                optionsList.push(aText);
                if (isCorrect) {
                    correctIndex = optionsList.length - 1; // Az aktuális (utolsó) index
                }
            }
        });

        if (optionsList.length < 2) {
            alert("Minden kérdéshez legalább 2 választ adj meg!");
            return;
        }

        if (correctIndex === -1) {
            alert(`A "${qText}" kérdéshez nem jelölted be a helyes választ!`);
            return;
        }

        // Hozzáadjuk a kérdést a listához a helyes struktúrával
        quizData.Questions.push({
            Text: qText,
            Options: optionsList,
            CorrectOptionIndex: correctIndex
        });
    }

    if (quizData.Questions.length === 0) {
        alert("Legalább egy kérdést adj hozzá!");
        return;
    }

    // Küldés a szervernek
    try {
        const response = await fetch('/Quiz/create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(quizData)
        });

        if (response.ok) {
            alert("Kvíz sikeresen elmentve!");
            showList(); // Vissza a listához
        } else {
            const errorText = await response.text();
            alert("Hiba mentéskor: " + errorText);
        }
    } catch (error) {
        console.error(error);
        alert("Hálózati hiba.");
    }
}