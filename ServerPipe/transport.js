

function TransportFile(file)
{

    //Send Transporter Start Request
    var bufTransportReqHeader = new ArrayBuffer(4);
    var TransportReqHeader = new Uint32Array(bufTransportReqHeader);
    TransportReqHeader[0] = 0x0001;

    var bufTransportReqLength = new ArrayBuffer(8);
    var TransportReqLength = new Uint32Array(bufTransportReqLength);
    TransportReqLength[0] = file.size;
    TransportReqLength[1] = 0x0000;

    var bufTransportReqCompress = new ArrayBuffer(1);
    var TransportReqCompress = new Uint8Array(bufTransportReqCompress);
    TransportReqCompress[0] = 0x00;

    var xhrStartUploadRequest = new XMLHttpRequest();
    xhrStartUploadRequest.open('POST', 'Receiver.aspx', false); // because of "false", will block until the request is done
    xhrStartUploadRequest.send(new Blob([TransportReqHeader, TransportReqLength, TransportReqCompress, '123456', '~/Uploads']));
    
    if (xhrStartUploadRequest.status != 200) {
        self.postMessage({'cmd': 'msg', 'msg': 'Call failure ' + xhrStartUploadRequest.status});
        return;
    }


    var strResponse = new String(xhrStartUploadRequest.response);
    var strResponses = strResponse.split("|");

    if (strResponses.length != 2) {
        self.postMessage({'cmd': 'msg', 'msg': 'Unexpected respnose from server : ' + strResponses});
        return;
    }
     if (strResponses[0] != 'OK') {
        self.postMessage({'cmd': 'msg', 'msg': 'Operation Failed : ' + strResponses});
        return;
    }


    var uploadSession = new String(strResponses[1]);


    if ('undefined' == uploadSession) {
        self.postMessage({'cmd': 'msg', 'msg': 'uploadSession is null'});
        return;
    }




    //Start Sending chunks
    TransportReqHeader[0] = 0xF0F0;
    var bufTransportReqOffset = new ArrayBuffer(8);
    var TransportReqOffset = new Uint32Array(bufTransportReqOffset);
    TransportReqOffset[0] = 0x0000;
    TransportReqOffset[1] = 0x0000;

    const SIZE = file.size;
    const BYTES_PER_CHUNK = 1024 *100;
    var start = 0;
    var end = BYTES_PER_CHUNK;
    
       while (start < SIZE)
    {
        var chunk = file.slice(start, end);
        
        TransportReqOffset[0] = start;
        
        var xhrChunkUploadRequest = new XMLHttpRequest();
        xhrChunkUploadRequest.open('POST', 'Receiver.aspx', false); 
        xhrChunkUploadRequest.send(new Blob([TransportReqHeader, TransportReqOffset, TransportReqCompress, uploadSession, '123456', chunk]));
        if (xhrChunkUploadRequest.status != 200) {
            self.postMessage({'cmd': 'error', 'msg': 'Call failure ' + xhrChunkUploadRequest.status});
            return;
        }
        strResponse = new String(xhrChunkUploadRequest.response);
        strResponses = strResponse.split("|");

        if (strResponses.length != 2) {
            self.postMessage({'cmd': 'error', 'msg': 'Unexpected respnose from server : ' + strResponses});
            return;
        }
         if (strResponses[0] != 'OK') {
            self.postMessage({'cmd': 'error', 'msg': 'Operation Failed : ' + strResponses});
             return;
       }
          
        start = end;
        self.postMessage({'cmd': 'progress', 'msg': ((start / SIZE) * 100)});
        //progressBar.textContent = progressBar.value; // Fallback for unsupported browsers.
        end = start + BYTES_PER_CHUNK;
    }





     //Finalize
    var xhrFinalizeUploadRequest = new XMLHttpRequest();
    xhrFinalizeUploadRequest.open('POST', 'Receiver.aspx', false); // because of "false", will block until the request is done

    TransportReqHeader[0] = 0xF0F0;
    TransportReqOffset[0] = -1;
    TransportReqOffset[1] = -1;

    ////self.postMessage('about to send LAST CHUNK' + filename);
    xhrFinalizeUploadRequest.send(new Blob([TransportReqHeader, TransportReqOffset, TransportReqCompress, uploadSession, '123456', '~/Uploads/' + file.name]));
    if (xhrFinalizeUploadRequest.status != 200) {
        self.postMessage({'cmd': 'error', 'msg': 'Call failure ' + xhrFinalizeUploadRequest.status});
        return;
    }

    strResponse = new String(xhrFinalizeUploadRequest.response);
    strResponses = strResponse.split("|");

    if (strResponses.length != 2) {
        self.postMessage({'cmd': 'error', 'msg': 'Unexpected respnose from server : ' + strResponses});
        return;
    }
     if (strResponses[0] != 'OK') {
        self.postMessage({'cmd': 'error', 'msg': 'Operation Failed : ' + strResponses});
         return;
   }

    //self.postMessage({'cmd': 'msg', 'msg': file.name + " succesfully uploaded"});
    
    

};



self.addEventListener('message', function(e) {

    
    
    var files = e.data;
   //self.postMessage({'cmd': 'msg', 'msg': files.length});
    for(i=0;i<files.length;i++ )
    {
        self.postMessage({'cmd': 'gui', 'msg': ('Transporting ' + files[i].name + '...')});
        TransportFile(files[i]);
        self.postMessage({'cmd': 'progress2', 'msg': ((i + 1)/files.length) * 100});
    }
   
      
      self.postMessage({'cmd': 'done', 'msg': 'Operation Complete'});
      return;

    
}, false);