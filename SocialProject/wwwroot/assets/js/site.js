//'use strict';

////var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

//connection.start().then(function () {
//    //GetAllUsers();
//    console.log("Connected");
//}).catch(function (err) {
//    return console.error(err.toString());
//});

function handlePhotoUpload() {
    const input = document.getElementById('photo-input');
    input.value = ''; // Reset the input to clear the previously selected files
    input.click();
}
function handleVideoUpload() {
    const input = document.getElementById('video-input');
    input.value = '';
    input.click();
}
document.getElementById('photo-input').addEventListener('change', function (e) {
    const selectedFiles = Array.from(e.target.files);
    const imagePreviewContainer = document.getElementById('image-preview-container');

    if (selectedFiles && selectedFiles.length > 0) {
        selectedFiles.forEach(file => {
            const reader = new FileReader();
            reader.onload = function (event) {
                const imagePreview = document.createElement('img');
                imagePreview.src = event.target.result;
                imagePreviewContainer.appendChild(imagePreview);
            };
            reader.readAsDataURL(file);
        });
    }
    this.value = '';
});

document.getElementById('video-input').addEventListener('change', function (e) {
    const selectedFiles = Array.from(e.target.files);
    const videoPreviewContainer = document.getElementById('video-preview-container');

    if (selectedFiles && selectedFiles.length > 0) {
        selectedFiles.forEach(file => {
            const videoPreview = document.createElement('video');
            videoPreview.src = URL.createObjectURL(file);
            videoPreview.controls = true;
            videoPreview.classList.add('video-preview'); // Add a class for styling
            videoPreviewContainer.appendChild(videoPreview);
        });
    }
    this.value = '';
});


function submitForm(action) {
    document.getElementById('uploadForm').setAttribute('action', action);
    document.getElementById('uploadForm').submit();
}


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
