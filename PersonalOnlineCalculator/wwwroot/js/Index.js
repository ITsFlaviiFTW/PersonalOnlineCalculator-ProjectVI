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
 
// Evaluate the expression and return the result
function calculate() {
    var p = document.getElementById("result").value;
    var q = eval(p);
    document.getElementById("result").value = q;

    addToHistory(p, q);
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

// This function deletes a row from the history table and sends an API DELETE call
function deleteRow(rowElement) {
    var historyTable = document.getElementById('historyBody');
    historyTable.removeChild(rowElement);

    
    // Send API DELETE call to delete the row from the database
    var expression = rowElement.firstChild.textContent.split(' = ')[0];
    var result = rowElement.firstChild.textContent.split(' = ')[1];
    var data = {
        expression: expression,
        result: result
    };

    fetch('/api/history', {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (response.ok) {
            console.log('Row deleted successfully');
        } else {
            console.log('Failed to delete row');
        }
    })
    .catch(error => {
        console.log('Error:', error);
    });
}
