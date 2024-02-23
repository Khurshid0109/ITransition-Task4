

// Function to update the hidden input with selected user IDs
const checkidUser = document.querySelector(".checkbox__input")
console.log(checkidUser);
usersidString = "";

const userId = [];
function funcCheckboxUser(id) {
    let found = false;
    if (userId.length === 0)
        userId.push(id)
    else {
        for (let i = 0; i < userId.length; i++) {
            if (userId[i] === id) {
                userId.splice(i, 1);
                found = true;
            }
        }
        console.log(userId);
        if (!found)
            userId.push(id);
    }
    usersidString = userId.join(",");

}

function blockSelected() {
    console.log(usersidString);

    $.ajax({
        url: '/Home/Block',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ selectedUserIds: usersidString }),
        success: function () {
            window.location.reload();
        }
    });
}

function unblockSelected() {
    console.log(usersidString);

    $.ajax({
        url: '/Home/UnBlock',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ selectedUserIds: usersidString }),
        success: function () {
            window.location.reload();
        }
    });
}

function deleteSelected() {
    console.log(usersidString);

    $.ajax({
        url: '/Home/Delete',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ selectedUserIds: usersidString }),
        success: function () {
            window.location.reload();
        }
    });
}