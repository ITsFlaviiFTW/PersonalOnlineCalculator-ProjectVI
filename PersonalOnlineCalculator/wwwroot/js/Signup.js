// Get the signup form element
const signupForm = document.querySelector(".signup-form-section");
// Add event listener for form submission
document.addEventListener('DOMContentLoaded', () => {
    const signupForm = document.querySelector(".signup-form-section");
    const submitButton = document.getElementById('signup-clkbtn');

    submitButton.addEventListener('click', (event) => {
        event.preventDefault(); // Prevent form submission

        // Get the input values
        const username = document.getElementById('name').value;
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        // Perform validation
        if (username.trim() === '' || email.trim() === '' || password.trim() === '') {
            alert('Please enter all required fields.');
            return;
        }

        // Create an object to store the user data
        const userData = {
            username: username,
            email: email,
            passwordhash: password
        };

        // Make a POST call to the server to add the user
        fetch('/api/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        })
            .then(response => response.json())
            .then(data => {
                // Handle the response from the server
                console.log(data);
                alert('Signup successful!');
                signupForm.reset();
                window.location.href = '/index.cshtml'; // Redirect to the home page
            })
            .catch(error => {
                // Handle any errors that occur during the signup process
                console.error(error);
                alert('An error occurred during signup. Please try again later.');
            });
    });
});