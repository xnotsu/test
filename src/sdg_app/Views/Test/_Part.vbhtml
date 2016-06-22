<table>
    <tr>
        <th>ID</th>
        <th>書籍名</th>
        <th>価格</th>
        <th>ISBN</th>
    </tr>

    @For Each item In ViewBag.Test
        Dim currentItem = item
        @<tr>
            <td>@item.id</td>
            <td>@item.Title</td>
            <td>@item.Price</td>
            <td>@item.Isbn</td>
        </tr>
    Next
</table>

