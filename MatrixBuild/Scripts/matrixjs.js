function ajax_download(url, data) {
    var $iframe,
        iframeDoc,
        iframeHtml;

    if (($iframe = $('#download_iframe')).length === 0) {
        $iframe = $("<iframe id='download_iframe'" + " style='display: none' src='about:blank'></iframe>").appendTo("body");
    }

    iframeDoc = $iframe[0].contentWindow || $iframe[0].contentDocument;
    if (iframeDoc.document) {
        iframeDoc = iframeDoc.document;
    }

    iframeHtml = "<html><head></head><body><form method='POST' action='" + url + "'>";

    Object.keys(data).forEach(function (key) {
        iframeHtml += "<input type='hidden' name='" + key + "' value='" + data[key] + "'>";

    });

    iframeHtml += "</form></body></html>";

    iframeDoc.open();
    iframeDoc.write(iframeHtml);
    $(iframeDoc).find('form').submit();
}