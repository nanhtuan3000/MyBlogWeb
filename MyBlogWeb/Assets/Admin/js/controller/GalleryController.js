var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $("#dataTable").DataTable({

            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 1,

            "ajax": {
                "url": "/Admin/Gallery/LoadData",
                "type": "POST",
                "datatype": "json"
            },

            "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [3],
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [4],
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [5],
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [6],
                "searchable": false,
                "orderable": false
            }],

            "columns": [
                  { "data": "ID", "name": "ID", "autoWidth": true },                  
                  { "data": "Name", "name": "Name", "autoWidth": true },
                  { "data": "Link", "name": "Link", "autoWidth": true },
                  { "data": "ImageUrl", "name": "ImageUrl", "autoWidth": true },
                  {
                      "render": function (data, type, full, meta)
                      { return '<a href="#" class="btn btn-info" onclick=ChangeStatus(' + full.ID + '); >' + (full.Status ? "Hiển thị" : "Ẩn") + '</a>'; }
                  },
                  {
                      "render": function (data, type, full, meta)
                      { return '<a class="btn btn-info" href="/Admin/Gallery/Edit/' + full.ID + '">Sửa</a>'; }
                  },
                  {
                      data: null, render: function (data, type, row) {
                          return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.ID + "'); >Xóa</a>";
                      }
                  }
            ]

        });       
    }    
}
user.init();

var loading = false;

function DeleteData(GalleryID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(GalleryID);
    }
    else {
        return false;
    }
}


function Delete(GalleryID) {
    var url = "/Admin/Gallery/DeleteGallery";
    $.post(url, { ID: GalleryID }, function (data) {
        if (data.deleted == true) {
            alert("Delete Gallery!");
            oTable = $('#dataTable').DataTable();
            oTable.draw();
        }
        else {
            alert("Something Went Wrong!");
        }
    });
}

function ChangeStatus(GalleryID) {
    var url = "/Admin/Gallery/ChangeStatus";
    var btn = $(this);
    $.post(url, { ID: GalleryID }, function (data) {
        if (data.status == true) {            
            btn.text = 'Hiển thị';            
        }
        else {
            btn.text = 'Ẩn';
        }
        oTable = $('#dataTable').DataTable();
        oTable.draw();
    });
}

function AjaxPost() {
    if (loading) {
        return;
    }
    loading = true;
    $('#AddImage').submit(function (e) {
        $(this).unbind("click");
        e.preventDefault();
        var Name = $('#txtName').val();
        var ImageUrl = $('#txtImageUrl').val();

        // check if the input is valid using a 'valid' property
        if ((Name.length == 0) || (ImageUrl.length == 0)) {
            return false;
        }
        
        var post_url = $(this).attr("action"); //get form action url
        var request_method = $(this).attr("method"); //get form GET/POST method
        var form_data = $(this).serialize(); //Encode form elements for submission

        $.ajax({
            url: post_url,
            type: request_method,
            data: form_data,
            cache: false,
            success: function (result) {
                alert(result);
                $('#txtName').val('');
                $('#txtLink').val('');
                $('#txtImageUrl').val('');
                oTable = $('#dataTable').DataTable();
                oTable.draw();
            },
            error: function () {
                loading = false;
            }
        })
        
    });
}
function BrowseServer() {
    // You can use the "CKFinder" class to render CKFinder in a page:
    var finder = new CKFinder();
    finder.basePath = '../';	// The path for the installation of CKFinder (default = "/ckfinder/").
    finder.selectActionFunction = SetFileField;
    finder.popup();

    // It can also be done in a single line, calling the "static"
    // popup( basePath, width, height, selectFunction ) function:
    // CKFinder.popup( '../', null, null, SetFileField ) ;
    //
    // The "popup" function can also accept an object as the only argument.
    // CKFinder.popup( { basePath : '../', selectActionFunction : SetFileField } ) ;
}

// This is a sample function which is called when a file is selected in CKFinder.
function SetFileField(fileUrl) {
    document.getElementById('txtImageUrl').value = fileUrl;
}
