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

// Get the login form element
const loginForm = document.getElementById('form-section');

// Add event listener for form submission
loginForm.addEventListener('submit', (event) => {
    event.preventDefault(); // Prevent form submission

    // Get the input values
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // Perform validation
    if (username.trim() === '' || password.trim() === '') {
        alert('Please enter both username and password.');
        return;
    }

    // Perform login logic here
    // You can make an API call or perform any other necessary actions

    loginForm.reset();
});
