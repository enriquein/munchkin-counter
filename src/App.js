import React from "react";
import ReactDOM from "react-dom";

// For prime controls
import "primereact/resources/themes/saga-blue/theme.css"
import "primereact/resources/primereact.min.css"
import "primeicons/primeicons.css"
import "primeflex/primeflex.css"

import PrimeReact from "primereact/api"
import { Button } from 'primereact/button';

PrimeReact.ripple = true

function App() {
    function onStartNewGameButtonHandler(evt) {
        // do whatever needs doing
    }

    return (
        <div>
        {/* if game hasnt started */}
            {/* render PlayerSetup */}
        {/* else */}
            <div>
                <div className="grid">
                    {/* loop through number of players to show a Player component for each */}
                </div>
                <div className="grid">
                    <div className="col-12">
                        <Button className="p-button-danger" label="Start new game" onClick={onStartNewGameButtonHandler} />
                    </div>
                </div>
            </div>
        </div>
    )
}

ReactDOM.render(<App />, document.getElementById('app'))