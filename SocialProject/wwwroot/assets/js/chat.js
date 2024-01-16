'use strict';
var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start().then(function () {
    //GetAllUsers();
    console.log("Connected");
}).catch(function (err) {
    return console.error(err.toString());
})

let element = document.querySelector("#alert");
connection.on("Connect", function (info) {
    console.log("Connect Work");
    //GetAllUsers();
    //element.style.display = "block";
    //element.innerHTML = info;
    //setTimeout(() => {
    //    element.innerHTML = "";
    //    element.style.display = "none";
    //}, 5000);
})
connection.on("Disconnect", function (info) {
    console.log("DisConnect Work");
    //GetAllUsers();
    //element.style.display = "block";
    //element.innerHTML = info;
    //setTimeout(() => {
    //    element.innerHTML = "";
    //    element.style.display = "none";
    //}, 5000);
})

function GetAllUsers() {

    $.ajax({
        url: "/Home/GetAllUsers",
        method: "GET",

        success: function (data) {
            let content = "";
            for (var i = 0; i < data.length; i++) {
                let dateContent = "";
                let style = '';
                let subContent = "";


                if (data[i].hasRequestPending) {
                    subContent = `<button  class='btn btn-outline-secondary'  onclick="TakeRequest('${data[i].id}')"> Already Sent</button 
                    >`;
                }
                else {
                    if (data[i].isFriend) {

                        subContent = `<button  class='btn btn-outline-secondary' onclick="UnFollowCall('${data[i].id}')"> UnFollow</button>`;
                    }
                    else {

                        subContent = `<button onclick="SendFollow('${data[i].id}')" class='btn btn-outline-primary'> Follow</button>`;
                    }
                }

                if (data[i].isOnline) {
                    style = 'border:5px solid springgreen;';
                }
                else {
                    style = 'border:5px solid red;';
                    let disconnectedDate = new Date(data[i].disconnectTime);
                    let currentDate = new Date();
                    let diffTime = Math.abs(currentDate - disconnectedDate);
                    let diffMinutes = Math.ceil(diffTime / (1000 * 60));
                    if (diffMinutes >= 60) {
                        diffMinutes = Math.ceil(diffMinutes / 60);
                        dateContent = `<span class='btn btn-warning' > Left ${diffMinutes} hours ago</span>`;
                    }
                    else {
                        dateContent = `<span class='btn btn-warning' > Left ${diffMinutes} minutes ago</span>`;
                    }

                }

                let item = `
                <div class='card' style='${style}width:14rem;margin:5px;'>
                    <img style='width:100%;height:220px;' src='/images/${data[i].imageUrl}'/>
                    <div class='card-body'>
                        <h5 class='card-title'>${data[i].userName}</h5>
                        <p class='card-text'>${data[i].email}</p>
                        ${subContent}
                        <p class='card-text mt-2'>${dateContent}</p>
                    </div>
                </div>
                `;
                content += item;
            }
            $("#allusers").html(content);
            GetFriends();
        }
    })
}

document.querySelectorAll('.chat-box').forEach(item => {
    item.addEventListener('click', event => {
        let userId = event.currentTarget.getAttribute('data-user-id');
        let username = event.currentTarget.getAttribute('data-username');
        let imageUrl = event.currentTarget.getAttribute('data-image-url');
        openChatForUser(userId, username, imageUrl);
    });
});


function openChatForUser(userId, username, imageUrl) {
    if (username != null) {
    document.querySelector('#userName').textContent = username;
    var imageElement = document.querySelector('#userImg');
    imageElement.src = imageUrl.includes('http') ? imageUrl : '/assets/images/user/' + imageUrl;

    document.querySelector('.live-chat-body').style.display = 'block';

    //fetch(`https://localhost:7129/Message/LiveChat/${userId}`)
    //    .then(response => response.json())
    //    .then(data => {
    //        // Assuming 'data' is an array of chat messages
    //        // Clear existing messages
    //        var chatContent = document.querySelector('.chat-content');
    //        chatContent.innerHTML = '';

    //        // Append each message to the chat content
    //        data.forEach(message => {
    //            var messageElement = document.createElement('div');
    //            messageElement.className = 'chat-message';
    //            messageElement.innerHTML = `
    //                <p>${message.text}</p>
    //                <span class="time d-block">${message.time}</span>
    //            `;
    //            chatContent.appendChild(messageElement);
    //        });
    //    })
    //    .catch(error => {
    //        console.error('Error fetching chat history:', error);
        //    });
    }
}


