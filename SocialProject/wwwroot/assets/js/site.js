﻿

function handlePhotoUpload() {
    const input = document.getElementById('photo-input');
    input.value = ''; 
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
            videoPreview.classList.add('video-preview'); 
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


(document).on('click', '.friendRequestButton', function () {
    const receiverId = $(this).data('receiverId');
    sendFriendRequest(receiverId);
});

function sendFriendRequest(receiverId, event) {
    const buttonElement = event.target;
    
    fetch(`/Profile/SendFriendRequest?id=${receiverId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (response.ok) {
                console.log('Friend request sent successfully');
                buttonElement.textContent = 'Pending';
                buttonElement.disabled = true; 
            } else {
                console.error('Failed to send friend request');
                buttonElement.textContent = 'Add Friend';
                buttonElement.disabled = false;
            }
        })
        .catch(error => {
            console.error('Error:', error);
            buttonElement.textContent = 'Add Friend';
            buttonElement.disabled = false;
        });
}
function AcceptRequest(id, id2, requestId) {
    $.ajax({
        url: `/Profile/AcceptRequest?userId=${id}&senderId=${id2}&requestId=${requestId}`,
        method: "GET",
        success: function (data) {
            let element = document.querySelector("#alert");
            element.style.display = "block";
            element.innerHTML = "You accept request successfully";
            GetAllUsers();
            SendFollowCall(id);
            SendFollowCall(id2);
            GetMyRequests();
            GetAllUsers();

            setTimeout(() => {
                element.innerHTML = "";
                element.style.display = "none";
            }, 5000);
        }
    })
}

function hideButtons() {
    document.getElementById("acceptButton").style.display = "none";
    document.getElementById("declineButton").style.display = "none";
}



