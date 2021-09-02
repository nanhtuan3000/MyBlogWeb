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
                "url": "/Admin/MessageBox/LoadData",
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
                "targets": [4],
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
                  { "data": "Detail", "name": "Detail", "autoWidth": true },                  
                  {
                      "render": function (data, type, full, meta)
                      {
                          if (full.CreateDate !== null){
                              return moment(full.CreateDate).format('DD-MM-YYYY HH:mm:ss');
                          } else {
                              return full.CreateDate
                          }
                      }
                  },
                  { "data": "Answer", "name": "Answer", "autoWidth": true },
                  {
                      "render": function (data, type, full, meta)
                      {
                          if (full.AnswerDate !== null) {
                              return moment(full.AnswerDate).format('DD-MM-YYYY HH:mm:ss');
                          } else {
                              return full.AnswerDate
                          }
                      }
                  },                  
                  { "data": "AnswerBy", "name": "AnswerBy", "autoWidth": true },
                  {
                      "render": function (data, type, full, meta)
                      { return '<a href="#" class="btn btn-info" >' + (full.Status ? "Đã trả lời" : "Đợi trả lời") + '</a>'; }
                  },
                  {
                      "render": function (data, type, full, meta)
                      { return '<a class="btn btn-info" href="/Admin/MessageBox/Edit/' + full.ID + '">Trả lời</a>'; }
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

function DeleteData(MessageID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(MessageID);
    }
    else {
        return false;
    }
}


function Delete(MessageID) {
    var url = "/Admin/MessageBox/DeleteMessage";
    $.post(url, { ID: MessageID }, function (data) {
        if (data.deleted == true) {
            alert("Delete Message!");
            oTable = $('#dataTable').DataTable();
            oTable.draw();
        }
        else {
            alert("Something Went Wrong!");
        }
    });
}

