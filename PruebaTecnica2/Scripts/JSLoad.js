$(document).ready(function () {


    //Call EmpDetails jsonResult Method
    $.getJSON("Home/EmpDetails",
        function (json) {
            var tr;
            //Append each row to html table
            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + json[i].Id + "</td>");
                tr.append("<td>" + json[i].Name + "</td>");
                tr.append("<td>" + json[i].City + "</td>");
                tr.append("<td>" + json[i].Address + "</td>");
                $('table').append(tr);
            }
        });
});