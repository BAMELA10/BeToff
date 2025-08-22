var connection = new signalR.HubConnectionBuilder()
    .withUrl("/Notification")
    .configureLogging(signalR.LogLevel.Information)
    .build();



async function start() {
    try {
        connection.on('ListOfInvitationForReceiver', (ListInvitation) => {
            console.log(ListInvitation);
            const tableBody = document.getElementById('Received');
            console.log(tableBody)
            const lines = document.getElementsByTagName('tr');
            console.log(lines);
            tableBody.innerHTML = ""
            /*lines.forEach(item => {
                return tableBody.removeChild(item);
            })*/
            ListInvitation.map(item => {
                if (item.status == "Pending") {
                    return tableBody.innerHTML += `
                <tr>
                    <td>${item.senderName}</td>
                    <td>${item.sendAt}</td>
                    <td>${item.familyName}</td>
                    <td>${item.expireAt}</td>
                    <td>${item.status}</td>
                    <td>
                        <a class="btn btn-primary" href="/Invitation/AcceptionInvitation/${item.id}">Accept</a>
                        <a class="btn btn-success" href="/Invitation/RefuseInvitation/${item.id}">Refuse</a>
                    </td>
                </tr>`
                } else {
                    return tableBody.innerHTML += `
                <tr>
                    <td>${item.senderName}</td>
                    <td>${item.sendAt}</td>
                    <td>${item.familyName}</td>
                    <td>${item.expireAt}</td>
                    <td>${item.status}</td>
                    <td> No Action Available</td>
                </tr>`
                }
                
            })
        });
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};+6

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();