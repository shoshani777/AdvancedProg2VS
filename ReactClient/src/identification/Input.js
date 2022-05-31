import React from "react";
import {Link} from "react-router-dom";
import './Input.css';

class Input extends React.Component{
    
    constructor(props) {
        super(props);
        this.inputSetter = props.inputSetter;
        this.cSetter = props.cSetter;
        this.type = props.type;
        this.id = props.id;
        this.description = props.description;
        this.eDescription = props.eDescription;
        this.cPassRef = props.cPassRef;
        this.cPassError = props.cPassError;
        this.input = React.createRef();
        this.validate = this.validate.bind(this);
        this.state = {
            regex : props.checkRegex,
            inputClass : "",
            error : ""
        };

    }
    
    setRegex = (regex) => {
        this.setState({
            regex : regex
        });
    };
    setValidity = (inputClass) => {
        this.setState({
            inputClass : inputClass
        });
    };
    setError = (error) => {
        this.setState({
            error : error
        });
    };

    validate(event) {
        const inputs = Array.prototype.slice.call(document.getElementsByTagName('input'));
        var cValue = this.input.current.value;
        if (this.id === 'pass') {
            this.cPassRef.current.setRegex('^' + cValue + '$');
            if (document.getElementById('cpass').value === '') {
                this.cPassRef.current.setValidity("");
                this.cPassRef.current.setError("");
                this.cSetter(null);
            }
            else if (document.getElementById('cpass').value === cValue) {
                this.cPassRef.current.setValidity("is-valid");
                this.cPassRef.current.setError("");
                this.cSetter(cValue);
            }
            else {
                this.cPassRef.current.setValidity("is-invalid");
                this.cPassRef.current.setError(this.cPassError);
                this.cSetter(null);
            }
        }
        if (this.id === 'cpass') {
            if (document.getElementById('cpass').value === '') {
                this.setState({
                    error : '',
                    inputClass : ''
                });
                this.inputSetter(null);
            }
        }
        const check = new RegExp(this.state.regex);
        if (check.test(cValue)) {
            this.setState({
                inputClass : "is-valid",
                error : ""
            });
            this.inputSetter(cValue);
            if (event.keyCode === 13) {
                inputs[(inputs.indexOf(event.target) + 1) % inputs.length].focus();
            }
            return;
        }
        this.inputSetter(null);
        this.setState({
            inputClass : "is-invalid",
            error : this.eDescription
        })
    }
    

    render() {
        
        //const errorLabal = document.getElementById(id + 'error');
        //const inputElement = document.getElementById(id); placeholder={placeholder}
        var shortenedNum=this.props.howShortened;
        var max = this.props.longInput?'25':'16';
        var input = typeof this.props.placeholder==='undefined'?<input data-lpignore="true" ref={this.input} id={this.id} onKeyUp={this.validate} type={this.type} 
        className={"form-control inputCls " + this.state.inputClass} onBlur={this.validate} maxLength={max}></input>
        :
        <input data-lpignore="true" ref={this.input} id={this.id} onKeyUp={this.validate} type={this.type} 
            className={"form-control inputCls " + this.state.inputClass} onBlur={this.validate} maxLength={max} placeholder={this.props.placeholder}></input>
        return (
        <div className="a0marginBtm bigDiv">
            <div className={'inputDiv'+shortenedNum}>
            {input}
            </div>
            <div id={this.id + 'error'} htmlFor={this.id} className={"text-danger err"+shortenedNum}>{this.state.error}</div>
        </div>
        );
    }
}
export default Input;