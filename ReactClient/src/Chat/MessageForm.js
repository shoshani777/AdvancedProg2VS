import React from 'react'
import './MessageForm.css'

class MessageForm extends React.Component {
  constructor(props) {
    super(props);
    this.onMessageSend = props.onMessageSend;
    // this.mediaRecorder = null;
    // this.shouldSend = false;
    this.textInput = <input type="text" ref={(node) => (this.input = node)} 
                      placeholder="Enter your message..." onKeyUp={this.typing}/>;
    this.sendText = <button className="bi bi-send" onClick={this.send}></button>
    // this.recordButton = <button className="bi bi-mic-fill" onClick={this.recordAudio}></button>;
    // this.deleteAudio = <button className="bi bi-trash" onClick={this.removeAudio}></button>;
    // this.stopRecording = <button className="bi bi-send-fill" onClick={this.stopAudio}></button>;
    // this.recordingInputArea = "recording..."
    
    // this.addFileButton = <button className="bi bi-image" onClick={this.addPic}></button>

    // this.picInputRef = React.createRef();

    // this.displayImageRef = React.createRef();
    // this.displayImage = <img className='display-image' ref={this.displayImageRef} id='displayImage' src='' alt="couldn't load" width="50" height="40" />

    // this.sendImageButton = <button className="bi bi-send-fill" onClick={this.sendPic}></button>
    
    // this.cancelImageButton = <button className="bi bi-trash" onClick={this.cancelPic}></button>
    // this.imageToSend = null;
    // this.state = {
    //   rightButton: this.recordButton,
    //   inputArea: this.textInput,
    //   leftButton: this.addFileButton
    // }
    
  }

  // sendPic = () => {
  //   this.onMessageSend(this.imageToSend, 'img');
  //   this.setState({
  //     rightButton: this.recordButton,
  //     inputArea: this.textInput,
  //     leftButton: this.addFileButton
  //   });
  //   this.imageToSend = null;
  // }

  // cancelPic = () => {
  //   this.imageToSend = null;
  //   this.setState({
  //     rightButton: this.recordButton,
  //     inputArea: this.textInput,
  //     leftButton: this.addFileButton
  //   });
  // }

  // addPic = () => {
  //   this.picInputRef.current.click();
  // }

  // display = (result) => {
  //   this.displayImageRef.current.src = result;
  //   this.imageToSend = result;
  // }

  // addedFile = () => {

  //   const file = this.picInputRef.current.files[0];
  //   const reader = new FileReader();

  //   reader.addEventListener("load", () => this.display(reader.result), false);
    
  //   if (file) {
  //     reader.readAsDataURL(file);
  //     this.setState({
  //       inputArea: this.displayImage,
  //       rightButton: this.sendImageButton,
  //       leftButton: this.cancelImageButton
  //     })
  //   }
  // }

  componentDidMount = () => {
    this.input.focus();
  }

  send = () => {
    if (this.input.value === '') {
        return;
    }
    this.onMessageSend(this.input.value);
    this.input.value = "";
    // this.setState({
    //   rightButton: this.recordButton
    // })
  }

  typing = (event) => {
    // if (this.input.value === '') {
    //   this.setState({
    //     rightButton: this.recordButton
    //   })
    // }
    // else {
    //   this.setState({
    //     rightButton: this.sendText
    //   })
    // }
    if (event.keyCode === 13) {
      this.send(event);
    }
  }

  // recordAudio = () => {
  //   this.setState({
  //     inputArea: this.recordingInputArea,
  //     rightButton: this.stopRecording,
  //     leftButton: this.deleteAudio
  //   });

  //   navigator.mediaDevices.getUserMedia({ audio: true }).then(stream => {
  //     const mediaRecorder = new MediaRecorder(stream);
  //     this.mediaRecorder = mediaRecorder;
  //     mediaRecorder.start();

  //     const audioChunks = [];
  //     mediaRecorder.addEventListener("dataavailable", event => {
  //       audioChunks.push(event.data);
  //     });

  //     mediaRecorder.addEventListener("stop", () => {
  //       this.mediaRecorder.stream.getTracks().forEach((track) => {
  //         track.stop();
  //       });
  //       const audioBlob = new Blob(audioChunks);
  //       const audioUrl = URL.createObjectURL(audioBlob);
  //       if (this.shouldSend) {
  //         this.onMessageSend(audioUrl, 'audio');
  //       }
  //       this.setState({
  //         inputArea: this.textInput,
  //         rightButton: this.recordButton,
  //         leftButton: this.addFileButton
  //       });
  //     });

      
  //   });
  // }

  // stopAudio = () => {
  //   if (this.mediaRecorder && this.mediaRecorder.state !== 'inactive') {
  //     this.shouldSend = true;
  //     this.mediaRecorder.stop();
  //   }
  // }
  // removeAudio = () => {
  //   if (this.mediaRecorder && this.mediaRecorder.state !== 'inactive') {
  //     this.shouldSend = false;
  //     this.mediaRecorder.stop();
  //   }
  // }

  render() {
    return (
      <div className="MessageForm">
        {/* <input type="file" accept="image/*" ref={this.picInputRef} onChange={this.addedFile} hidden/> */}
        {/* <div className="left-button-container">
          {this.state.leftButton}
        </div> */}
        <div className="input-container">
          {/* {this.state.inputArea} */}
          {this.textInput}
        </div>
        <div className="right-button-container">
          {/* {this.state.rightButton} */}
          {this.sendText}
        </div>
      </div>
    )
  }
}

export default MessageForm