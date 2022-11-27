import { useState } from "react";
import { FormLabel, Stack, useMediaQuery, useTheme } from "@mui/material";
import "prismjs/themes/prism-coy.css";
import AceEditor from "react-ace";
import "ace-builds/src-noconflict/mode-html";
import "ace-builds/src-noconflict/theme-monokai";
import "ace-builds/src-noconflict/ext-language_tools";

function HTMLEditor() {
  const isDesktop = useMediaQuery(useTheme().breakpoints.up("md"));
  const [code, setCode] = useState("<h1>Hello World!</h1>");

  // useEffect(() => {
  //   getHtml();
  // }, []);

  console.log(code);

  return (
    <Stack>
      <FormLabel htmlFor="html-editor">Enter HTML Input</FormLabel>
      <Stack
        sx={{
          m: 2,
        }}
      >
        <AceEditor
          wrapEnabled
          placeholder="Enter your HTML"
          mode="html"
          theme="monokai"
          name="blah2"
          fontSize={14}
          showGutter={true}
          value={code}
          onChange={(value) => setCode(value)}
          onPaste={(value) => setCode(value)}
          setOptions={{
            enableBasicAutocompletion: true,
            enableLiveAutocompletion: false,
            enableSnippets: false,
            showLineNumbers: true,
            tabSize: 2,
          }}
          style={{
            width: isDesktop ? "700px" : "85vw",
          }}
        />
      </Stack>
    </Stack>
  );
}

export default HTMLEditor;
