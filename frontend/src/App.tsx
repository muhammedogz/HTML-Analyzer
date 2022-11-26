import Sa from "components/Sa";
import { useState } from "react";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="App">
      <Sa />
    </div>
  );
}

export default App;
