mergeInto(LibraryManager.library,
    {
        InitFileLoader: function (callbackObjectName, callbackMethodName) {

            FileCallbackObjectName = UTF8ToString(callbackObjectName);
            FileCallbackMethodName = UTF8ToString(callbackMethodName);

            var fileuploader = document.getElementById('fileuploader');
            if (!fileuploader) {
                console.log('Creating fileuploader...');
                fileuploader = document.createElement('input');
                fileuploader.setAttribute('style', 'display:none;');
                fileuploader.setAttribute('type', 'file');
                fileuploader.setAttribute('id', 'fileuploader');
                fileuploader.setAttribute('class', 'nonfocused');
                document.getElementsByTagName('body')[0].appendChild(fileuploader);

                fileuploader.onchange = function (e) {
                    var files = e.target.files;

                    if (files.length === 0) {
                        ResetFileLoader();
                        return;
                    }

                    console.log('ObjectName: ' + FileCallbackObjectName + ';\nMethodName: ' + FileCallbackMethodName + ';');
                    SendMessage(FileCallbackObjectName, FileCallbackMethodName, URL.createObjectURL(files[0]));
                };
            }

            console.log('FileLoader initialized!');
        },



        RequestUserFile: function (extensions) {
            var str = UTF8ToString(extensions);
            var fileuploader = document.getElementById('fileuploader');

            if (fileuploader === null)
                InitFileLoader(FileCallbackObjectName, FileCallbackMethodName);

            if (str !== null || str.match(/^ *$/) === null)
                fileuploader.setAttribute('accept', str);

            fileuploader.setAttribute('class', 'focused');
            fileuploader.click();
        },

        ResetFileLoader: function () {
            var fileuploader = document.getElementById('fileuploader');

            if (fileuploader) {
                fileuploader.setAttribute('class', 'nonfocused');
            }
        },
    });