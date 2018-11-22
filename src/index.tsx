import "./index.css";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { ConnectedPageComponent } from "./components/page";
import { rootReducer } from "./reducers";
import { createStore } from "redux";

export const store = createStore(rootReducer);

ReactDOM.render(
    <ConnectedPageComponent store={store} />,
    document.getElementById("root")
);
