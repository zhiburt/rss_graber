$(document).ready(function() {
    $.ajax({
        url: "/api/rss",
        type: "GET",
        success: success,
        dataType: "json",
        error: function (r) {
            alert("ERROR", r);
        }
    });
});

const TABLE_BODY_ID = "#table_body";
const CONTAINER_ID = "#container";

function success(data) {
    var d = $(data);
    if (d.length > 0) {
        $(CONTAINER_ID).append(`<div class="row">
        <div class="controls col col-12 pb-4 pt-4">

        </div>
        <div class="col-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Origin</th>
                                    <th scope="col">Title</th>
                                    <th scope="col">Description</th>
                                    <th scope="col">Date</th>
                                </tr>
                            </thead>
                            <tbody id="table_body"></tbody>
                        </table>
                    </div>
                </div>
                <div class="card-footer">
                    <div class="row">
                    </div>
                </div>
            </div>
        </div>
    </div>`);

        d.each(function (index) {
            $(TABLE_BODY_ID).append(RssToRow(index, this));
        });
        $(TABLE_BODY_ID).after(`<caption>Count ` + d.length + `</caption>`);

    } else {
        $(CONTAINER_ID).append(`<h3 class="pt-5 pb-5"> <mark>Not found<mark/> any RSS </h3>`);
    }
}

function RssToRow(i, rss) {
    rss.description = rss.description.replace(/<\/?[^>]+(>|$)/g, "");
    return `<tr>
        <th scope="row">`+ i + `</th>
        <td style="height:10px; overflow:hidden"><a href="`+ rss.linkToOriginal + `">` + rss.linkToOriginal.substr(0, 30) + `</a></td>
        <td style="height:10px; overflow:hidden">` + rss.title.substr(0, 30) + `</td>
        <td style="height:10px; overflow:hidden">` + rss.description.substr(0, 30) + `</td>
        <td style="height:10px; overflow:hidden">` + rss.date + `</td>
    </tr>`;
}