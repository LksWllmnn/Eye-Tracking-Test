namespace frontendStudiArb {
    
    let host: string = "";

    let ipButton: HTMLButtonElement = <HTMLButtonElement>document.getElementById("setIP");
    if (ipButton) ipButton.addEventListener("click", getData);

    let ipInput: HTMLInputElement = <HTMLInputElement>document.getElementById("ip");

    

    async function communicate(): Promise<void> {
        //172.29.4.34
        let path: string = "/api/app/packagemanager/packages";
        let url: string = "https://192.168.50.86/portal/echo?args=hello";
        try {
            let response: Response = await fetch("http://192.168.50.86/portal/echo?args=foo", {
                keepalive: true,
                method: "GET",
                
                headers: {
                  "Content-Type": "application/json"
                }});
            //let response2: string = await response.json();
            console.log(response);
        } catch (e: any) {
            console.log(e);
        }
        
    }

    function getData(): void {
        host = ipInput.value;
        console.log("hi");
        communicate();
    }
}