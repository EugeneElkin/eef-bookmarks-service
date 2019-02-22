import "./index.css";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { PageComponent } from "./components/page";
import { rootReducer } from "./state/reducers";
import { createStore } from "redux";

export const store = createStore(rootReducer);

ReactDOM.render(
    <PageComponent />,
    document.getElementById("root")
);
