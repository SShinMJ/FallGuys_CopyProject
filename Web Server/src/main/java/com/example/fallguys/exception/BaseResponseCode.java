package com.example.fallguys.exception;

import lombok.AllArgsConstructor;
import lombok.Getter;
import org.springframework.http.HttpStatus;

@Getter
@AllArgsConstructor
public enum BaseResponseCode {

    /**
     * 200 OK : 요청 성공
     */
    OK(HttpStatus.OK, "요청 성공하였습니다."),

    /**
     * 400 BAD_REQUEST : 잘못된 요청
     */
    BAD_REQUEST(HttpStatus.BAD_REQUEST, "잘못된 요청입니다."),
    INVALID_PASSWORD(HttpStatus.BAD_REQUEST, "잘못된 비밀번호입니다. 다시 입력해주세요."),
    DUPLICATE_EMAIL(HttpStatus.BAD_REQUEST, "중복된 이메일입니다. 다시 입력해주세요."),
    DUPLICATE_NICKNAME(HttpStatus.BAD_REQUEST, "중복된 닉네임입니다. 다시 입력해주세요."),

    /**
     * 404 NOT FOUND
     */
    USER_NOT_FOUND(HttpStatus.NOT_FOUND, "사용자를 찾을 수 없습니다."),
    USER_COLOR_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 사용자 색상 소유 정보 데이터를 찾을 수 없습니다."),
    COSTUME_COLOR_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 색상 데이터를 찾을 수 없습니다."),

    /**
     * 404 NOT FOUND
     */
    FAILED_TO_SAVE_USER(HttpStatus.NOT_FOUND, "사용자 등록에 실패했습니다."),

    /**
     * 405 Method Not Allowed
     */
    METHOD_NOT_ALLOWED(HttpStatus.METHOD_NOT_ALLOWED, "해당 사용자에게 허용되지 않은 메서드입니다.");

    private HttpStatus httpStatus;
    private String message;
}
