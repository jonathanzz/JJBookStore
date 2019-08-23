
function qclick(flag, i, unit) {
    currentTotal = parseFloat(document.getElementById("tt").innerHTML);
    if (flag) {
        document.getElementById(i).value++;
        if (document.getElementById(i + "check").checked) {
            document.getElementById("tt").innerHTML = (currentTotal + unit).toFixed(2);
        }
    }
    else {
        if (document.getElementById(i).value > 1) {
            document.getElementById(i).value--;
            if (document.getElementById(i + "check").checked) {
                document.getElementById("tt").innerHTML = (currentTotal - unit).toFixed(2);
            }
        }
    }
    document.getElementById(i + "+subtotal").innerHTML = (document.getElementById(i).value * unit).toFixed(2);
}

function total(i) {
    currentTotal = parseFloat(document.getElementById("tt").innerHTML);
    if (document.getElementById(i + "check").checked) {
        document.getElementById("tt").innerHTML = (currentTotal + parseFloat(document.getElementById(i + "+subtotal").innerHTML)).toFixed(2);
    } else {
        document.getElementById("tt").innerHTML = (currentTotal - parseFloat(document.getElementById(i + "+subtotal").innerHTML)).toFixed(2);
    }
}

function selall() {
    var checkboxes = document.getElementsByTagName("input");
    if (document.getElementById("selectall").checked) {
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].type != "checkbox" || checkboxes[i].id == "selectall")
                continue;
            subtotalid = checkboxes[i].id.substring(0, checkboxes[i].id.length - 5) + "+subtotal";
            if (!checkboxes[i].checked) {
                document.getElementById("tt").innerHTML = (parseFloat(document.getElementById("tt").innerHTML) +
                    parseFloat(document.getElementById(subtotalid).innerHTML)).toFixed(2);
                document.getElementById(checkboxes[i].id).checked = true;
            }
        }
    } else {
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].type != "checkbox")
                continue;
            checkboxes[i].checked = false;
        }
        document.getElementById("tt").innerHTML = 0.00.toFixed(2);
    }
}

$("input[type=submit]").click(function (event) {
    if ($(".checkboxes:checked").length < 1) {
        alert("Please select at least one item.");
        event.preventDefault();
        return;
    } else if (this.id == "purchasenow") {
        window.open('http://localhost:1234/Purchaseds/PaymentConfirm');
        //confirm not working at Google Chrome, because of https://www.chromestatus.com/feature/5140698722467840
        return confirm('Have you finised your payment successfully?');
    } else if (this.id == "remove") {
        return confirm('Do you really want to remove those items?')
    }

});
