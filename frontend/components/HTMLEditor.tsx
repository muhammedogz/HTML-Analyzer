import Editor from "react-simple-code-editor";
import { useState } from "react";
import "prismjs/themes/prism-coy.css";
import { highlight, languages } from "prismjs";

function HTMLEditor() {
  const [code, setCode] = useState("<h1>Hello World!</h1>");
  return (
    <Editor
      value={code}
      onValueChange={(code) => setCode(code)}
      highlight={(code) => highlight(code, languages.html, "html")}
      padding={10}
      style={{
        fontFamily: '"Fira code", "Fira Mono", monospace',
        fontSize: 12,
      }}
    />
  );
}

export default HTMLEditor;
