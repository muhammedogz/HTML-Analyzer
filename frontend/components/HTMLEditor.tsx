import Editor from "react-simple-code-editor";
import { useState } from "react";
import { highlight, languages } from "prismjs";
import { FormControl, FormLabel, Stack } from "@mui/material";
import "prismjs/themes/prism-coy.css";

function HTMLEditor() {
  const [code, setCode] = useState("<h1>Hello World!</h1>");

  return (
    <Stack>
      <FormControl>
        <FormLabel htmlFor="html-editor">Enter HTML Input</FormLabel>
        <Stack
          sx={{
            ".npm__react-simple-code-editor__textarea": {
              border: "1px solid #000 !important",
            },
          }}
        >
          <Editor
            id="html-editor"
            value={code}
            onValueChange={(code) => setCode(code)}
            highlight={(code) => highlight(code, languages.html, "html")}
            padding={10}
            style={{
              fontFamily: '"Fira code", "Fira Mono", monospace',
              fontSize: 12,
            }}
            placeholder="Enter HTML here"
          />
        </Stack>
      </FormControl>
    </Stack>
  );
}

export default HTMLEditor;
