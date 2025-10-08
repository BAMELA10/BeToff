
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/Chat")
    .configureLogging(signalR.LogLevel.Information)
    .build();


//async function connected() {
//    await connection.invoke("OnConnectedAsync")
//    console.log("Connected");
//}

async function start() {
    //fonction for display list of message on pages
    connection.on("Message", (message) => {
        var dico = sessionStorage.getItem("Dico")
        let keyConversation = message.conversation.toString();
        var jsonDico = JSON.parse(dico)
        var participant = jsonDico[keyConversation];
        var userSender = participant[0]
        var userRevceiver = participant[1]
        var discussionArea = document.getElementById('discussion');
        let time = message.sendAt;
        var date = time.split("T");
        var dateDay = date[0].replaceAll("-", "/")
        let newdate = dateDay.split("/").reverse().join('/')

        var timeDay = date[1].split(".");   
        time = timeDay[0]

        if (message.senderId === userSender.Id) {

            discussionArea.innerHTML += `
                <div class="row justify-content-end">
                    <div class="col-5 col-md-6">
                        <div class="card mb-2 mt-3">
                            <div class="card-body">
                                <h5 class="card-title">${userSender.FullName}</h5>
                                <p class="card-text">${message.content}</p>
                                <p class="card-text"><small class="text-body-secondary">${newdate} ${time}</small></p>
                            </div>
                        </div>
                    </div>
                </div>
            `
        }
        if (message.senderId === userRevceiver.Id) {
            discussionArea.innerHTML += `
                <div class="row justify-content-start">
                    <div class="col-5 col-md-6">
                        <div class="card mb-2 mt-3">
                            <div class="card-body">
                                <h5 class="card-title">${userRevceiver.FullName}</h5>
                                <p class="card-text">${message.content}</p>
                                <p class="card-text"><small class="text-body-secondary">${newdate} ${time}</small></p>
                            </div>
                        </div>
                    </div>
                </div>`
        }
    })

    connection.on("CachingMessage", (Dico) => {
        sessionStorage.setItem("Dico", Dico);
        var dico = sessionStorage.getItem("Dico")
    })

    

    await connection.start();
    console.log("SignalR Connected.");

}


//async function disconnected() {
//    await connection.invoke("OnDisconnectedAsync")
//    console.log("Disconnected");
//}
//fonction for send call send message fonction of Hub

document.getElementById("submit").addEventListener('click', (event) => {
    let url = window.location.pathname.split("/");
    const conversation = url[url.length - 1];
    let input = document.getElementById("content")
    let content = input.value;
    let clearValue = content.trim();

    if (content && clearValue.length != 0) {
        connection.invoke("SendMessage", conversation, content);
    }
    input.textContent = "";
    event.preventDefault();
})

//document.getElementById("input").addEventListener('', (event) => {
//    var button = document.getElementById("submit");
//    button.removeAttribute("disabled");
//})


connection.onclose(async () => {
    await start();
});

start();