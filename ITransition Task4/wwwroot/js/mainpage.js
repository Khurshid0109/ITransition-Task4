
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
        },
        error: function (xhr, status, error) {
            if (xhr.status === 428) {
                // User has blocked themselves, redirect to the login page
                window.location.href = '/Access/Login';

                alert(xhr.responseText);
            }
            else {
                console.error(xhr.responseText);
                alert('An error occurred while processing the request.');
            }
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
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
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
        },
        error: function (xhr, status, error) {
            if (xhr.status === 403) {
                // User has deleted themselves, redirect to the register page
                window.location.href = '/Access/Register';

                alert(xhr.responseText);
            }
            else {
                console.error(xhr.responseText);
                alert('An error occurred while processing the request.');
            }
        }
    });
}