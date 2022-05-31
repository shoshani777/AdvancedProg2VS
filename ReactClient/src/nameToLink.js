const nameToLink = new Map([
    ['',{name:'logIn',linkTo:'register',strLinkTo:"Haven't registered?",bg:'./identificationBG.css',exitCss:'logInExit'}],
    ['logIn',{name:'logIn',linkTo:'register',strLinkTo:"Haven't registered?",bg:'./identificationBG.css',exitCss:'logInExit'}],
    ['register',{name:'register',linkTo:'logIn',strLinkTo:"I already have an account",bg:'./identificationBG.css',exitCss:'registerExit'}],
    ['webPage',{name:'webPage',linkTo:'logIn',strLinkTo:
        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-box-arrow-left exitIcon" viewBox="0 0 16 16">
            {/* <path fill-rule="evenodd" d="M6 12.5a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 0-.5-.5h-8a.5.5 0 0 0-.5.5v2a.5.5 0 0 1-1 0v-2A1.5 1.5 0 0 1 6.5 2h8A1.5 1.5 0 0 1 16 3.5v9a1.5 1.5 0 0 1-1.5 1.5h-8A1.5 1.5 0 0 1 5 12.5v-2a.5.5 0 0 1 1 0v2z"/>
            <path fill-rule="evenodd" d="M.146 8.354a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L1.707 7.5H10.5a.5.5 0 0 1 0 1H1.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3z"/> */}
            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
            <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>

        </svg>
    ,bg:'./webPageBG.css',exitCss:'webPageExit'}]
]);

export default nameToLink;