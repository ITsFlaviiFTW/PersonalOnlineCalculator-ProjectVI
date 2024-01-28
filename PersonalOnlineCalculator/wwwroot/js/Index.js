// This function clears all the values
function clearScreen() {
    document.getElementById("result").value = "";
}

let equalsPressedLast = false;

// This function displays the values
function display(value) {
    // If the last key pressed was '=', clear the screen before displaying the new value
    if (equalsPressedLast) {
        document.getElementById("result").value = "";
        equalsPressedLast = false; // Reset the flag
    }
    document.getElementById("result").value += value;
}
 
function calculate() {
    var expression = document.getElementById("result").value;

    // Send expression to server for calculation
    fetch('/Calculator/Calculate', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ expression: expression })
    })
        .then(response => response.json())
        .then(data => {
            document.getElementById("result").value = data.result;
            // Assuming the server returns an object with a 'result' property
            addToHistory(expression, data.result);
        })
        .catch(error => {
            console.error('Error:', error);
        });

    equalsPressedLast = true;
}

// This function updates the history table
function addToHistory(expression, result) {
    var historyTable = document.getElementById('historyBody');
    var newRow = historyTable.insertRow();
    var newCell = newRow.insertCell();
    newCell.className = 'history-row'; 

    var textNode = document.createTextNode(`${expression} = ${result}`);
    newCell.appendChild(textNode);

    // Creating delete button image
    var deleteButton = document.createElement('img');
    deleteButton.src = 'Img/red-x.png';
    deleteButton.alt = 'Delete';
    deleteButton.className = 'delete-button';
    deleteButton.style.width = '15px';
    deleteButton.style.height = '15px';
    deleteButton.addEventListener('click', function() {
        deleteRow(newRow);
    });

    newCell.appendChild(deleteButton);
}

// This function deletes a row from the history table
function deleteRow(rowElement) {
    var historyTable = document.getElementById('historyBody');
    historyTable.removeChild(rowElement);
}

