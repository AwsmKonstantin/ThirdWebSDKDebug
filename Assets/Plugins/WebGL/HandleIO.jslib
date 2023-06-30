var HandleIO = {
    _WindowAlert : function(message)
    {
        window.alert(Pointer_stringify(message));
    },
    _SyncFiles : function()
    {
        FS.syncfs(false,function (err) {
            console.log(err);
        });
    }
};

mergeInto(LibraryManager.library, HandleIO);