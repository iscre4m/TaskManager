let resetForms = () => {
    document.getElementById("signUpForm").reset();
    for (let item of document.getElementById("signUpValidation").childNodes) {
        item.innerHTML = "";
    }

    document.getElementById("signInForm").reset();
    for (let item of document.getElementById("signInValidation").childNodes) {
        item.innerHTML = "";
    }
}

document.getElementById("signUpModal").addEventListener("hidden.bs.modal", resetForms);
document.getElementById("signInModal").addEventListener("hidden.bs.modal", resetForms);

