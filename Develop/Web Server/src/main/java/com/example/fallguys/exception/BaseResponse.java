package com.example.fallguys.exception;

import com.fasterxml.jackson.annotation.JsonInclude;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Getter;
import org.springframework.http.HttpStatus;

@Getter
@AllArgsConstructor
public class BaseResponse<T> {

    @Schema(name = "HttpStatus Code", example = "OK")
    private HttpStatus httpStatus;

    @Schema(name = "응답 메시지", example = "요청 성공하였습니다.")
    private String message;

    @Schema(name = "응답 result")
    @JsonInclude(JsonInclude.Include.NON_NULL)
    private T data;
}
