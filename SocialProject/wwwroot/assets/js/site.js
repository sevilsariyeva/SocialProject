'use strict';

//var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    //GetAllUsers();
    console.log("Connected");
}).catch(function (err) {
    return console.error(err.toString());
});

function handlePhotoUpload() {
    document.getElementById('photo-input').click();
}

document.getElementById('photo-input').addEventListener('change', function (e) {
    const selectedFiles = Array.from(e.target.files);
    const imageInput = document.getElementById('image-input');
    let imagesData = ''; 

    selectedFiles.forEach(file => {
        const reader = new FileReader();
        reader.onload = function () {

            imageInput.src = reader.result;
        };
        reader.readAsDataURL(file);
    });

    e.target.value = '';
});

document.getElementById('uploadForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const formData = new FormData(this);
    fetch('@Url.Action("Upload", "Home")', {
        method: 'POST',
        body: formData
    }).then(response => {
        console.log('Upload successful');
        window.location.href = '@Url.Action("Index", "Home")';
    }).catch(error => {
        console.error('Error:', error);
    });
});
