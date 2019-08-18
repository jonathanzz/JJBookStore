function qclick(flag, i, unit) {
    currentTotal = parseFloat(document.getElementById("total").innerHTML);
    if (flag) {
        document.getElementById(i).value++;
        if (document.getElementById(i + "check").checked) {
            document.getElementById("total").innerHTML = (currentTotal + unit).toFixed(2);
        }
    }
    else {
        if (document.getElementById(i).value > 1) {
            document.getElementById(i).value--;
            if (document.getElementById(i + "check").checked) {
                document.getElementById("total").innerHTML = (currentTotal - unit).toFixed(2);
            }
        }
    }
    document.getElementById(i + "+subtotal").innerHTML = (document.getElementById(i).value * unit).toFixed(2);
}

function total(i) {
    currentTotal = parseFloat(document.getElementById("total").innerHTML);
    if (document.getElementById(i + "check").checked) {
        document.getElementById("total").innerHTML = (currentTotal + parseFloat(document.getElementById(i + "+subtotal").innerHTML)).toFixed(2);
    } else {
        document.getElementById("total").innerHTML = (currentTotal - parseFloat(document.getElementById(i + "+subtotal").innerHTML)).toFixed(2);
    }
}