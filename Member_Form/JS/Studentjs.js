function Hello() {

}

var isPhoneValid = false;


function ValidatePhoneNumber() {
    debugger;
    var phone = document.getElementById('txtPhoneNumber').value;
    if (!validatePhoneFormat(phone)) {

        alert("Invalid phone number")
    } else {
        isPhoneValid = true;
    }
}

function validatePhoneFormat(input_str) {
    var re = /^[0-9]{10}$/;

    return re.test(input_str);
}
var URL = "https://Student/api/Studenttest";
var data = {};

data.Name = document.getElementById('txtName').value;
data.PhoneNumber = document.getElementById('txtPhoneNumber').value;
data.Gender = document.getElementById('txtGender').value;
data.Address = document.getElementById('txtAddress').value;
data.Photo = document.getElementById('txtPhoto').value;


var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function () {
    if (this.readyState == 4) {
        if (this.status == 200) {
            alert(this.responseText);

            var Id = "";
            const obj = JSON.parse(this.responseText);
            if (obj && obj.Status && obj.Status == "Success") {
                Id = obj.Message;
                Id = Id.replace("Success Id: ", "");

                var URL = "https://Student/MyHTML/Studenthtml.html"
                var dynamicURL = URL + "?Id=" + Id;
                window.location.href = dynamicURL;
            }
        }
        else
            alert(this.response)
    }
};
xhttp.open("POST", URL, true);
xhttp.setRequestHeader("Content-type", "application/json");
xhttp.send(JSON.stringify(data));
}