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
                "url": "/Admin/User/LoadData",
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
                "targets": [5],
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [6],
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [7],
                "searchable": false,
                "orderable": false
            }],

            "columns": [
                  { "data": "ID", "name": "ID", "autoWidth": true },
                  { "data": "UserName", "name": "UserName", "autoWidth": true },
                  { "data": "Name", "name": "Name", "autoWidth": true },
                  { "data": "Phone", "name": "Phone", "autoWidth": true },
                  { "data": "Email", "name": "Email", "autoWidth": true },
                  {
                      "render": function (data, type, full, meta)
                      { return '<a href="#" class="btn btn-info" onclick=ChangeStatus(' + full.ID + '); >' + (full.Status ? "Kích hoạt" : "Khóa") + '</a>'; }
                  },
                  {
                      "render": function (data, type, full, meta)
                      { return '<a class="btn btn-info" href="/Admin/User/Edit/' + full.ID + '">Sửa</a>'; }
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

function DeleteData(CustomerID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(CustomerID);
    }
    else {
        return false;
    }
}


function Delete(CustomerID) {
    var url = "/Admin/User/DeleteUser";
    $.post(url, { ID: CustomerID }, function (data) {
        if (data.deleted == true) {
            alert("Delete User!");
            oTable = $('#dataTable').DataTable();
            oTable.draw();
        }
        else {
            alert("Something Went Wrong!");
        }
    });
}

function ChangeStatus(CustomerID) {
    var url = "/Admin/User/ChangeStatus";
    var btn = $(this);
    $.post(url, { ID: CustomerID }, function (data) {
        if (data.status == true) {            
            btn.text = 'Kích hoạt';            
        }
        else {
            btn.text = 'Khóa';
        }
        oTable = $('#dataTable').DataTable();
        oTable.draw();
    });
}