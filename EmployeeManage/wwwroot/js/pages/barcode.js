
window.onload = pageLoad();


function closeCustomerModal() {

    $('#closeButton').click();

}
function closeOrderModal() {

    $('#closeOrderButton').click();

}

function pageLoad() {
    startTime();
    setTimeout(function () {
        getWalkInCustomer();
        getItemsData();

        if (document.getElementById("layout-Body").classList.contains('sidebar-mini')) {
            document.getElementById("layout-Body").classList.toggle('sidebar-collapse');
        }
    }, 0001);

}
       

function getHoldOrderData() {

    var holdedid = document.getElementById("OrderHoldId").value;
    $.ajax({
        type: "POST",
        url: "getHoldOrder",
        data: { id: holdedid },
        dataType: 'json',
        success: function (response) {

            if (response == '[]') {
                
                $('#modal-ordernotfound').modal('show');

                document.getElementById("OrderHoldId").value = "";
                document.getElementById("OrderHoldId").focus();
                return false;
            }
            else {
                
                var data = JSON.parse(response);
                $('#itemTable').empty();
                document.getElementById("OrderHoldId").value = "";
                document.getElementById('lbl_TotalAmount').innerText = ".0000";
                document.getElementById('lbl_CashAmount').value = ".0000";
                document.getElementById('lbl_balance').innerText = ".0000";

                for (i = 0; i < data.length; i++) {
                    CreateTable(data[i].ItemId, data[i].ItemName, data[i].Quantity, data[i].Price, data[i].ItemBarcode);
                }
                document.getElementById("lbl_customerName").innerText = data[0].CustomerName;
                document.getElementById("lbl_customerPhoneNo").innerText = data[0].PhoneNumber;
                document.getElementById("lbl_customerId").innerText = data[0].CustomerId;
                
                closeOrderModal();
            }
        },
        error: function (req, status, error) {
            console.log("error" + error);
        }
    });
}

function getWalkInCustomer() {
    $.ajax({
        type: "POST",
        url: "getWalkinCustomerData",
        data: { id: "wlk1" },
        dataType: "text",
        success: function (result) {
            var customerData = JSON.parse(result);

            document.getElementById("lbl_customerName").innerText = customerData.customerName;
            document.getElementById("lbl_customerPhoneNo").innerText = customerData.phoneNumber;
            document.getElementById("lbl_customerId").innerText = customerData.customerId;
        },
        error: function (req, status, error) {
            console.log("error" + error);
        }
    });
}


function SaveCustomerData() {

    var customerName = document.getElementById("customerName").value;
    var customerPhoneNumber = document.getElementById("customerPhonenumber").value;
    var customerNTN = document.getElementById("Ntn").value;
    var customerCnic = document.getElementById("Cnic").value;
    $.ajax({
        type: "POST",
        url: "SaveCustomer", 
        data: { Name: customerName, phoneNo: customerPhoneNumber, Ntn: customerNTN, Cnic: customerCnic },
        dataType: "text",
        success: function (response) {
            
            getCustomerData(response);
            document.getElementById("customerName").value = "";
            document.getElementById("customerPhonenumber").value = "";
            document.getElementById("Ntn").value = "";
            document.getElementById("Cnic").value = "";
            closeCustomerModal();


            $('#Modal-customer-Save').modal('show');

        },
        error: function (req, status, error) {
            console.log("error" + error);
        }
    });

}

function getCustomerData(value) {
    $.ajax({
        type: "POST",
        url: "getCustomer",
        data: { id: value },
        dataType: "text",
        success: function (response) {

            var Data = JSON.parse(response);

            document.getElementById("lbl_customerName").innerText = Data.customerName;
            document.getElementById("lbl_customerPhoneNo").innerText = Data.phoneNumber;
            document.getElementById('lbl_customerId').innerText = value;
        },
        error: function (req, status, error) {
            console.log("error" + error);
        }
    });

}

function checkCashAmount() {
    var cashAmount = document.getElementById('lbl_CashAmount').value;
    var totalAmount = document.getElementById('lbl_TotalAmount').innerText;
    
    if (parseFloat(cashAmount) < parseFloat(totalAmount)) {
        $('#Modal-Cash').modal('show');
        return false;
    }
    return true;
}

function onlyNumberKey(evt) {

    // Only ASCII character in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57)) {
        return false;
    }
    return true;
}

function Balancecalculate() {

    var totalAmount = document.getElementById('lbl_TotalAmount').innerText;
    var cashAmount = document.getElementById('lbl_CashAmount').value;
    document.getElementById('lbl_balance').innerText = parseFloat(cashAmount - totalAmount).toFixed(4);
}

function checkbox(value) {
    var checkboxes = document.getElementsByName('check')

    checkboxes.forEach((item) => {        
        if (item !== value)
            item.checked = false
    })
}

function getCheckboxValue() {
    var box1 = document.getElementById("c1");
    var box2 = document.getElementById("c2");
    var box3 = document.getElementById("c3");
    var result = " ";
    if (box1.checked == true) {
        var bx1 = document.getElementById("c1").value;
        result = bx1 ;
    }
    else if (box2.checked == true) {
        var bx2 = document.getElementById("c2").value;
        result = bx2 ;
    }
    else if (box3.checked == true) {
        document.write(result);
        var bx3 = document.getElementById("c3").value;
        result = bx3;
    }
    else {
        $('#Modal-SelectCheckbox').modal('show');
    }
    return result;

}



function startTime() {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var d = new Date();
    var dayName = days[d.getDay()];
    const today = new Date();
    let h = today.getHours();
    var ampm = h >= 12 ? 'PM' : 'AM';
    h = h % 12;
    h = h ? h : 12; // the hour '0' should be '12'
    let m = today.getMinutes();
    let s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById("dateid").innerHTML = dayName + "|" + h + ":" + m + ":" + s + "|" + ampm;
    setTimeout(startTime, 1000);
}

function checkTime(i) {
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}

function getItemsData() {
    $.ajax({
        type: "GET",
        url:"BarcodeLoad",
        dataType: 'text',
        success: function (data) {
            
            document.getElementById("lbl_items").innerText = data;
            console.log("success:" + "Products Loaded")

        },
        error: function (req, status, error) {
            console.log("error" + "Products Not Loaded");
        }
    });
}


function searchBarcode() {

    var data = document.getElementById("lbl_items").innerText;


    var data = JSON.parse(data);


    for (i = 0; i < data.length; i++) {


        let quantity = 1;

        if (data[i].itemBarcode === $("#txtBarcode").val()) {
          
            CreateTable(data[i].itemId, data[i].itemName, quantity, data[i].price, data[i].itemBarcode, data[i].percentage, data[i].discount);

            return true;
        }
    }

    $('#modal-Notfound').modal('show');


    return false;
}

function Save(orderedItems, totalAmount, cashAmount, Balance, customerID) {
    $.ajax({
        type: "POST",
        url: "SaveOrder",
        data: { orderedItems: JSON.stringify(orderedItems), TotalAmount: totalAmount, CashAmount: cashAmount, balance: Balance, CustomerId: customerID },
        dataType: "text",
        success: function (response) {
            
            $('#Modal-order-Save').modal('show');

            document.getElementById('txtBarcode').value = "";
            document.getElementById('lbl_TotalAmount').innerText = "0.0000";
            document.getElementById('lbl_CashAmount').value = "0.0000";
            document.getElementById('lbl_balance').innerText = "0.0000";

            document.getElementById("lbl_customerName").innerText = "";
            document.getElementById("lbl_customerPhoneNo").innerText = "";
            document.getElementById('lbl_customerId').innerText = "";
            getWalkInCustomer();

            $('#itemTable').empty();
        },
        error: function (req, status, error) {
            console.log("error" + error);
        }
    });
}

    function HoldOrder(orderedItems, totalAmount, customerID, HoldKey) {
        $.ajax({
            type: "POST",
            url: "HoldOrder",
            data: { orderedItems: JSON.stringify(orderedItems), TotalAmount: totalAmount, CustomerId: customerID, Holdkey: HoldKey },
            dataType: "text",
            success: function (response) {
                $('#modal-HoldSuccessFully').modal('show');

                document.getElementById('txtBarcode').value = "";
                document.getElementById('lbl_TotalAmount').innerText = ".0000";
                document.getElementById('lbl_CashAmount').value = ".0000";
                document.getElementById('lbl_balance').innerText = ".0000";
                document.getElementById("lbl_customerName").innerText = "";
                document.getElementById("lbl_customerPhoneNo").innerText = "";
                document.getElementById('lbl_customerId').innerText = "";

                getWalkInCustomer();

                
                $('#itemTable').empty();
            },
            error: function (req, status, error) {
                console.log("error" + error);
            }
        });
    }

    function HoldOrderItemsCheck() {
        var totalAmount = document.getElementById("lbl_TotalAmount").innerText;
        var customerID = document.getElementById("lbl_customerId").innerText;
        var tableArray = storeTblValues();
        
        var HoldKey = Date.now().toString(36) + Date.now();
        if (tableArray.length === 0) {

            
            $('#Modal-ItemSelection').modal('show');

            return false;
        }

        HoldOrder(tableArray, totalAmount, customerID, HoldKey);
    }

function FormValidation() {
    var totalAmount = document.getElementById("lbl_TotalAmount").innerText;
    var cashAmount = document.getElementById("lbl_CashAmount").value;
    var Balance = document.getElementById("lbl_balance").innerText;
    var customerID = document.getElementById('lbl_customerId').innerText;

    var tableArray = storeTblValues();
    if (tableArray.length === 0) {
        $('#Modal-ItemSelection').modal('show');

        document.getElementById("txtBarcode").focus();
        return false;
    }
    if (cashAmount === "0.0000") {
        $('#Modal-Cash').modal('show');
        document.getElementById("lbl_CashAmount").focus();
        return false;
    }
    console.log(getCheckboxValue());
    if (getCheckboxValue() !== "cash") {
        $('#Modal-checkbox').modal('show');
        return false;
    }
    if (checkCashAmount()) {
        Save(tableArray, totalAmount, cashAmount, Balance, customerID);

    }
}

    function storeTblValues() {
        var tableData = new Array();
        $('#itemTable tr').each(function (row, tr) {
            tableData[row] = {
                "ID": $(tr).find('td:eq(1)').text(),
                "QTY": $(tr).find('td:eq(3)').text(),
                "PRICE": $(tr).find('td:eq(4)').text(),
                "AMOUNT": $(tr).find('td:eq(5)').text()
            }
        });

        return tableData;
    }

    function DeleteRow(value) {
        var tbl = document.getElementById("itemTable");
        var row = value.parentNode.parentNode;
        row.parentNode.removeChild(row);
        CalculateSum(tbl);

    }

function Calculation(value) {
    var taxAmount = 0;
    var netAmount = 0;
    var grossAmount = 0;
    var discount = 0;

    for (var r = 0, n = value.rows.length; r < n; r++) {
        taxAmount = taxAmount + parseFloat(value.rows[r].cells[7].innerHTML);
        grossAmount = grossAmount + (parseFloat(value.rows[r].cells[5].innerHTML));
        discount = discount + parseFloat(value.rows[r].cells[8].innerHTML);
        netAmount = (netAmount + (parseFloat(value.rows[r].cells[5].innerHTML) + parseFloat(value.rows[r].cells[7].innerHTML)) - parseFloat(value.rows[r].cells[8].innerHTML));
    }

    document.getElementById("lbl_TotalAmount").innerHTML = netAmount.toFixed(4);
    document.getElementById('lbl_SubTotal').innerText = grossAmount.toFixed(4);

    document.getElementById("lbl_TaxAmount").innerHTML = taxAmount.toFixed(4);
    document.getElementById("lbl_Discount").innerHTML = discount.toFixed(4);
}

function CreateTable(id, Name, quantity, Price, barcode, taxAmount, discount) {
    // creates a <table> element and a <tbody> element
    let sr = 1;

    var tbl = document.getElementById("itemTable");
    // creates a table row
    var row = document.createElement("tr");
    var SR = document.createElement("td");
    var cId = document.createElement("td");
    var cName = document.createElement("td");
    var cQuantity = document.createElement("td");
    var cPrice = document.createElement("td");
    var cAmount = document.createElement("td");
    var cBarcode = document.createElement("td");
    var cTaxAmount = document.createElement("td");
    var cDiscount = document.createElement("td");
    var cTotal = document.createElement("td");
    var cDelete = document.createElement("td");

    SR.setAttribute("align", "center");
    SR.setAttribute("width", "80px");
    cName.style.width = "250px";
    cQuantity.setAttribute("align", "right");
    cQuantity.setAttribute("width", "100px");
    cPrice.setAttribute("align", "right");
    cPrice.setAttribute("width", "120px");
    cAmount.setAttribute("align", "right");
    cAmount.setAttribute("width", "150px");
    cTaxAmount.setAttribute("align", "right");
    cTaxAmount.setAttribute("width", "150px");
    cDiscount.setAttribute("width", "150px");
    cDiscount.setAttribute("align", "right");
    cTotal.setAttribute("width", "150px");
    cTotal.setAttribute("align", "right");
    cDelete.setAttribute("align", "center");
    cDelete.setAttribute("width", "100px");


    let a = false;

    var qty = 0;
    var amount = 0;
    var tax = 0;
    var discountAmount = 0;
    var totalAmount = 0;

    for (var r = 0, n = tbl.rows.length; r < n; r++) {

        if (tbl.rows[r].cells[6].innerHTML === $("#txtBarcode").val()) {

            qty = (parseFloat(tbl.rows[r].cells[3].innerHTML) + 1).toFixed(4);
            tbl.rows[r].cells[3].innerHTML = qty;
            amount = (parseFloat(Price) * qty).toFixed(4);
            tbl.rows[r].cells[5].innerHTML = amount; //amount
            tax = ((taxAmount / 100) * amount).toFixed(4);
            tbl.rows[r].cells[7].innerHTML = tax; //tax amount
            discountAmount = (qty * discount).toFixed(4);
            tbl.rows[r].cells[8].innerHTML = discountAmount; // discount amount
            console.log(amount + tax);
            tbl.rows[r].cells[9].innerHTML = ((parseFloat(amount) + parseFloat(tax)) - parseFloat(discountAmount)).toFixed(4);

            Calculation(tbl);

            a = true;
            break;
        }
        sr++;
    }
    if (!a) {
        SR.innerHTML = sr;
        cId.innerHTML = id;
        cBarcode.innerHTML = barcode;
        cName.innerHTML = Name;
        cQuantity.innerHTML = quantity.toFixed(4);
        cPrice.innerHTML = Price.toFixed(4);
        cAmount.innerHTML = (Price * quantity).toFixed(4);
        cBarcode.innerHTML = barcode;
        cTaxAmount.innerHTML = ((taxAmount / 100) * cAmount.innerHTML).toFixed(4);
        cDiscount.innerHTML = discount;


        cTotal.innerHTML = (((Price * quantity) + ((taxAmount / 100) * cAmount.innerHTML)) - cDiscount.innerHTML).toFixed(4);

        cDelete.innerHTML = "<a data-toggle='tooltip' title='Delete' onclick='DeleteRow(this)'><li class='fa fa-trash'></li></a>";

        row.appendChild(SR);
        row.appendChild(cId).style.display = 'none';
        row.appendChild(cName);
        row.appendChild(cQuantity);
        row.appendChild(cPrice);
        row.appendChild(cAmount);
        row.appendChild(cBarcode).style.display = 'none';
        row.appendChild(cTaxAmount);
        row.appendChild(cDiscount);
        row.appendChild(cTotal);
        row.appendChild(cDelete);
        tbl.appendChild(row);

        Calculation(tbl);
    }
}

  