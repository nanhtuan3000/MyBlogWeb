/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
    config.filebrowserBrowseUrl = '/Assets/Plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/Plugins/ckfinder/ckfinder.html?Types=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/Plugins/ckfinder/ckfinder.html?Types=Flash';
    config.filebrowserUploadUrl = '/Assets/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
    config.filebrowserImageUploadUrl = '/UploadFiles';
    config.filebrowserFlashUploadUrl = '/Assets/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.removePlugins = 'easyimage,cloudservices,exportpdf';

    CKFinder.setupCKEditor(null, '/Assets/Plugins/ckfinder/');

};
