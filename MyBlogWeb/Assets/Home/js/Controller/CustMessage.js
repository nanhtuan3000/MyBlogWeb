$('.btnSubmit').click(function () {
    
        var UserName = $("#UserName").val();        
        var Detail = CKEDITOR.instances['textMessage'].getData();
        var MessageModel = {
            UserName: UserName,
            Detail: Detail,
        }

        $.ajax({
            url: '/CustMessage/Create',
            type: 'POST',
            data: JSON.stringify(MessageModel),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.success == true) {
                    window.location.href = "/CustMessage/Index";
                }
                else if (data.success == false) {
                    alert("Error occured..!!")
                }
            },
            error: function () {
                alert("Error occured..!!");
            },
        });
    
});