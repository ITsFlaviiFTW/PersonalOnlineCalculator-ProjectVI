
let signup = document.querySelector(".signup");
let login = document.querySelector(".login");
let slider = document.querySelector(".loginSlider");
let formSection = document.querySelector(".login-form-section");
 
signup.addEventListener("click", () => {
    slider.classList.add("moveslider");
    formSection.classList.add("form-section-move");
});
 
login.addEventListener("click", () => {
    slider.classList.remove("moveslider");
    formSection.classList.remove("form-section-move");
});

// Get the login button
const loginButton = document.getElementById('login-clkbtn');

loginButton.addEventListener('click', async (event) => {
    event.preventDefault(); // Prevent form submission

    // Get the input values
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Perform validation
    if (email.trim() === '' || password.trim() === '') {
        alert('Please enter both username and password.');
        return;
    }

    try {
        // Make an API call to check the username and password with the database
        const response = await fetch('/api/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        });

        if (response.ok) {
            // Redirect the user to the index.cshtml page
            window.location.href = '@Url.Action("Index", "Home")';
            // window.location.href = '/index.cshtml';
        } else {
            // If the response is not ok, show an error message
            alert('Invalid username or password.');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while logging in.');
    }
});